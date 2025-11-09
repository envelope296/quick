using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Quick.BusinessLogic.Services.Abstractions;
using Quick.Common.Configuration.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Quick.BusinessLogic.Services.Implementations
{
    public class JwtGenerationService : IJwtGenerationService
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly JwtOptions _jwtOptions;

        public JwtGenerationService(
            IDateTimeProvider dateTimeProvider,
            IOptionsMonitor<JwtOptions> jwtOptions)
        {
            _dateTimeProvider = dateTimeProvider;
            _jwtOptions = jwtOptions.CurrentValue;
        }

        public string GenerateJwtToken(long userId)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, userId.ToString()),
            };

            var currentDateTime = _dateTimeProvider.GetUtcNow();
            var expires = currentDateTime.AddHours(_jwtOptions.ExpirationHours);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: expires,
                notBefore: currentDateTime,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),
                    SecurityAlgorithms.HmacSha256
                )
            );

            var jwtSecurityHandler = new JwtSecurityTokenHandler();
            return jwtSecurityHandler.WriteToken(token);
        }
    }
}
