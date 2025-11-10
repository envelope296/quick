using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Quick.API.Controllers
{
    [ApiController]
    [Route("v1/teachers")]
    [Authorize]
    public class TeacherController : ControllerBase
    {
        public TeacherController()
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
