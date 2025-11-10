using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Quick.API.Controllers
{
    [ApiController]
    [Route("v1/subjects")]
    [Authorize]
    public class SubjectController : ControllerBase
    {
        public SubjectController()
        {

        }

        [HttpPost("/page")]
        public async Task<IActionResult> GetPageAsync([FromBody] object request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("/{id:guid}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        public async Task<IActionResult> CreateAsync([FromBody] object request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
