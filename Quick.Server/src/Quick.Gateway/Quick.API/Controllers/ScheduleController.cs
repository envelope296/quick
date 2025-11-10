using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Quick.API.Controllers
{
    [ApiController]
    [Route("v1/schedules")]
    [Authorize]
    public class ScheduleController : ControllerBase
    {
        public ScheduleController()
        {

        }

        [HttpGet("/{id:guid}")]
        public async Task<IActionResult> GetAsync([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        [HttpPost("/page")]
        public async Task<IActionResult> GetPageAsync([FromBody] object request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] object request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        [HttpPatch("/{id:guid}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] object request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
