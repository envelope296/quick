using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quick.BusinessLogic.Contracts.Requests.Subgroups;
using Quick.BusinessLogic.Contracts.Responses.Common;
using Quick.BusinessLogic.Contracts.Responses.Subgroups;
using Quick.BusinessLogic.Repositories.Abstractions;
using Quick.Common.Context.UserContext;
using Quick.DataAccess.Context.Extensions;
using Quick.DataAccess.Models;

namespace Quick.API.Controllers
{
    [ApiController]
    [Route("v1/subgroups")]
    [Authorize]
    public class SubgroupController : ControllerBase
    {
        private readonly IUserContextAccessor _userContextAccessor;
        private readonly ISubgroupRepository _subgroupRepository;

        public SubgroupController(
            IUserContextAccessor userContextAccessor,
            ISubgroupRepository subgroupRepository)
        {
            _userContextAccessor = userContextAccessor;
            _subgroupRepository = subgroupRepository;
        }

        [HttpGet("page")]
        public async Task<ActionResult<PageResponse<SubgroupResponse>>> GetPageAsync(
            [FromQuery] Guid groupId, 
            [FromQuery] int page, 
            [FromQuery] int size, 
            CancellationToken cancellationToken)
        {
            // TODO: Validation

            var subgroupsPage = await _subgroupRepository
                .Filter(s => s.GroupId == groupId)
                .OrderBy(s => s.Name)
                .Select(s => new SubgroupResponse
                {
                    Id = s.Id,
                    Name = s.Name,
                })
                .ToPageResponseAsync(page, size, cancellationToken);

            return Ok(subgroupsPage);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> AddAsync([FromBody] CreateSubgroupRequest request, CancellationToken cancellationToken)
        {
            // TODO: Validation

            var subgroup = new Subgroup
            {
                GroupId = request.GroupId,
                Name = request.Name,
            };
            await _subgroupRepository.ExecuteAddAsync(subgroup, cancellationToken);

            return Ok(subgroup.Id);
        }

        [HttpDelete("{subgroupId:guid}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid subgroupId, CancellationToken cancellationToken)
        {
            // TODO: Validation

            await _subgroupRepository.ExecuteDeleteAsync(subgroupId, cancellationToken);
            return Ok();
        }
    }
}
