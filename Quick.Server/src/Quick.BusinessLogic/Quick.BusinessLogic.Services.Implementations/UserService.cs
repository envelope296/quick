using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Quick.BusinessLogic.Contracts.Dto.Users;
using Quick.BusinessLogic.Contracts.Exceptions.Users;
using Quick.BusinessLogic.Contracts.Requests.Users;
using Quick.BusinessLogic.Repositories.Abstractions;
using Quick.BusinessLogic.Services.Abstractions;
using Quick.Common.Configuration.Options;
using Quick.Common.Helpers.Crypto;
using Quick.DataAccess.Models;

namespace Quick.BusinessLogic.Services.Implementations
{
    public class UserService : IUserService
    {
        private static string InitDataHashKey = "hash";
        private static string InitDataUserKey = "user";
        private static string SecretKeyMessage = "WebAppData";

        private readonly IJwtGenerationService _jwtGenerationService;
        private readonly IUserRepository _userRepository;
        private readonly BotOptions _botOptions;

        public UserService(
            IJwtGenerationService jwtGenerationService, 
            IUserRepository userRepository,
            IOptionsMonitor<BotOptions> botOptions)
        {
            _jwtGenerationService = jwtGenerationService;
            _userRepository = userRepository;
            _botOptions = botOptions.CurrentValue;
        }

        public async Task<string> GenerateTokenAsync(GenerateUserTokenRequest request, CancellationToken cancellationToken)
        {
            var parsedInitData = ParseInitData(request.InitData);
            if (!ValidateInitData(parsedInitData)) 
            {
                throw new InvalidInitDataException();
            }

            var userData = GetValueFromInitData(parsedInitData, InitDataUserKey);
            var user = ParseWebAppUser(userData);

            long? userId = await _userRepository.GetIdAsync(u => u.MessagerUserId == user.Id, cancellationToken);
            if (!userId.HasValue)
            {
                var newUser = new User { MessagerUserId = user.Id };
                _userRepository.Add(newUser);
                await _userRepository.SaveChangesAsync(cancellationToken);
                userId = user.Id;
            }

            return _jwtGenerationService.GenerateJwtToken(userId.Value);
        }

        private static Dictionary<string, string> ParseInitData(string initData)
        {
            var decodedInitData = Uri.UnescapeDataString(initData);
            return decodedInitData
                .Split('&')
                .Select(part => part.Split('='))
                .Where(parts => parts.Length == 2)
                .ToDictionary(parts => parts[0], parts => parts[1]);
        }

        private bool ValidateInitData(Dictionary<string, string> initData)
        {
            var hash = GetValueFromInitData(initData, InitDataHashKey);
            var initDataPairs = initData
                .Where(pair => pair.Key != InitDataHashKey)
                .OrderBy(pair => pair.Key)
                .Select(pair => $"{pair.Key}={pair.Value}");
            var initDataString = string.Join('\n', initDataPairs);

            var secretKey = CryptoHelper.HmacSha256(SecretKeyMessage, _botOptions.Token);
            var result = CryptoHelper.HmacSha256(secretKey, initDataString);
            var checkHash = Convert.ToHexString(result);

            return checkHash.Equals(hash, StringComparison.OrdinalIgnoreCase);
        }

        private static WebAppUserDto ParseWebAppUser(string userData)
        {
            try
            {
                var user = JsonConvert.DeserializeObject<WebAppUserDto>(userData);
                return user;
            }
            catch (Exception e)
            {
                throw new InvalidInitDataException();
            }
        }

        private static string GetValueFromInitData(Dictionary<string, string> initData, string key)
        {
            if (initData.TryGetValue(key, out var value))
            {
                return value;
            }
            throw InvalidInitDataException.MissingRequiredKey(key);
        }
    }
}
