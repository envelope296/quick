using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Quick.Common.Context.UserContext
{
    public class ClaimsUserContextAccessor : BaseUserContextAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClaimsUserContextAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public override UserContext? GetCurrent()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
            {
                return null;
            }

            var user = httpContext.User;
            if (user == null)
            {
                return null;
            }

            var isAuthenticated = user.Identity?.IsAuthenticated ?? false;
            if (isAuthenticated)
            {
                return null;
            }

            var nameIdentifierClaim = user.FindFirst(ClaimTypes.NameIdentifier);
            if (nameIdentifierClaim != null && long.TryParse(nameIdentifierClaim.Value, out var userId))
            {
                return new UserContext
                {
                    UserId = userId,
                };
            }

            return null;
        }
    }
}
