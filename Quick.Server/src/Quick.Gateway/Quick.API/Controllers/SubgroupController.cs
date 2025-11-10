using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Quick.API.Controllers
{
    [ApiController]
    [Route("v1/subgroups")]
    [Authorize]
    public class SubgroupController : ControllerBase
    {
        [HttpPost("/page")]
        public async Task<IActionResult> GetPageAsync([FromBody] object request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] object request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
