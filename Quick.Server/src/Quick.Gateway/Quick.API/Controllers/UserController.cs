using Microsoft.AspNetCore.Mvc;
using Quick.BusinessLogic.Contracts.Requests.Users;
using Quick.BusinessLogic.Services.Abstractions;

namespace Quick.API.Controllers
{
    [ApiController]
    [Route("v1/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("token")]
        public async Task<ActionResult<string>> GenerateTokenAsync([FromBody] GenerateUserTokenRequest request, CancellationToken cancellationToken)
        {
            var token = await _userService.GenerateTokenAsync(request, cancellationToken);
            return Ok(token);
        }
    }
}
