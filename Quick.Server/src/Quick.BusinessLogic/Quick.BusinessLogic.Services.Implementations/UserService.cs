using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Quick.BusinessLogic.Contracts.Dto.Users;
using Quick.BusinessLogic.Contracts.Exceptions.Users;
using Quick.BusinessLogic.Contracts.Requests.Users;
using Quick.BusinessLogic.Repositories.Abstractions;
using Quick.BusinessLogic.Services.Abstractions;
using Quick.Common.Configuration.Options;
using Quick.Common.Helpers.Crypto;
using Quick.DataAccess.Models;
using System.Web;

namespace Quick.BusinessLogic.Services.Implementations
{
    public class UserService : IUserService
    {
        private static string InitDataHashKey = "hash";
        private static string InitDataUserKey = "user";
        private static string SecretKeyMessage = "WebAppData";

        private static JsonSerializerSettings WebAppUserSerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            }
        };

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
            //if (!ValidateInitData(parsedInitData)) 
            //{
            //    throw new InvalidInitDataException();
            //}

            var userData = GetValueFromInitData(parsedInitData, InitDataUserKey);
            var user = ParseWebAppUser(userData);
            var userId = await CreateOrUpdateAsync(user, cancellationToken);

            return _jwtGenerationService.GenerateJwtToken(userId);
        }

        private async Task<long> CreateOrUpdateAsync(WebAppUserDto webAppUser, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(u => u.MessagerUserId == webAppUser.Id, cancellationToken);
            if (user == null)
            {
                var newUser = new User { 
                    MessagerUserId = webAppUser.Id,
                    FirstName = webAppUser.FirstName,
                    LastName = webAppUser.LastName,
                    UserName = webAppUser.Username,
                    PhotoUrl = webAppUser.PhotoUrl,
                };
                await _userRepository.ExecuteAddAsync(newUser, cancellationToken);
                return newUser.Id;
            }

            user.FirstName = webAppUser.FirstName;
            user.LastName = webAppUser.LastName;
            user.UserName = webAppUser.Username;
            user.PhotoUrl = webAppUser.PhotoUrl;

            await _userRepository.SaveChangesAsync(cancellationToken);
            return user.Id;
        }

        private static Dictionary<string, string> ParseInitData(string initData)
        {
            var decodedInitData = HttpUtility.UrlDecode(initData);
            return decodedInitData
                .Split('&')
                .Select(part => part.Split('=', 2))
                .ToDictionary(
                    parts => parts[0], 
                    parts => HttpUtility.UrlDecode(parts[1])
                );
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
                var user = JsonConvert.DeserializeObject<WebAppUserDto>(userData, WebAppUserSerializerSettings);
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
