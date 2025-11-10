using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quick.DataAccess.Context.Extensions;
using Quick.BusinessLogic.Contracts.Requests.Groups;
using Quick.BusinessLogic.Contracts.Responses.Common;
using Quick.BusinessLogic.Contracts.Responses.Groups;
using Quick.BusinessLogic.Repositories.Abstractions;
using Quick.Common.Context.UserContext;
using Quick.DataAccess.Models;

namespace Quick.API.Controllers
{
    [ApiController]
    [Route("v1/groups")]
    [Authorize]
    public class GroupController : ControllerBase
    {
        private readonly IUserContextAccessor _userContextAccessor;
        private readonly IGroupRepository _groupRepository;
        private readonly IGroupMemberRepository _groupMemberRepository;

        public GroupController(
            IUserContextAccessor userContextAccessor,
            IGroupRepository groupRepository,
            IGroupMemberRepository groupMemberRepository)
        {
            _userContextAccessor = userContextAccessor;
            _groupRepository = groupRepository;
            _groupMemberRepository = groupMemberRepository;
        }

        [HttpPost("/page")]
        public async Task<ActionResult<PageResponse<GroupResponse>>> GetPageAsync([FromBody] GetGroupsPageRequest request, CancellationToken cancellationToken)
        {
            var userContext = _userContextAccessor.GetCurrentOrFail();
            var groupsPage = await _groupRepository.Filter(g =>
                g.OwnerId == userContext.UserId ||
                g.Members.Any(gm => gm.UserId == userContext.UserId)
            )
            .OrderByDescending(g => g.IsPublic)
            .ThenBy(g => g.CreatedOn)
            .Select(g => new GroupResponse
            {
                Name = g.Name,
                Description = g.Description,
                OwnerId = g.OwnerId,
                IsPublic = g.IsPublic,
            })
            .ToPageResponseAsync(request, cancellationToken);
            return Ok(groupsPage);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateAsync([FromForm] CreateGroupRequest request, CancellationToken cancellationToken)
        {
            var userContext = _userContextAccessor.GetCurrentOrFail();
            var group = new Group
            {
                Name = request.Name,
                Description = request.Description,
                OwnerId =  userContext.UserId,
                IsPublic = true
            };
            await _groupRepository.ExecuteAddAsync(group, cancellationToken);
            return Ok(group.Id);
        }

        [HttpPost("/{id:guid}/members/page")]
        public async Task<ActionResult<PageResponse<GroupMemberResponse>>> GetMembersPageAsync([FromRoute] Guid id, [FromQuery] int page, [FromQuery] int size, CancellationToken cancellationToken)
        {
            var groupMembers = await _groupMemberRepository
                .Filter(gm => gm.GroupId == id)
                .Select(gm => new GroupMemberResponse
                {

                })
                .ToPageResponseAsync(page, size, cancellationToken);
            return Ok(groupMembers);
        }

        [HttpPost("/{id:guid}/members")]
        public async Task<ActionResult> AddMemberAsync([FromRoute] Guid groupId, [FromBody] AddGroupMemberRequest request, CancellationToken cancellationToken)
        {
            var groupMember = new GroupMember
            {
                GroupId = groupId,
                UserId = request.UserId,
                SubgroupId = request.SubgroupId
            };
            await _groupMemberRepository.ExecuteAddAsync(groupMember, cancellationToken);
            return Ok();
        }

        [HttpDelete("/{groupId:guid}/members/{userId:long}")]
        public async Task<IActionResult> DeleteMemberAsync([FromRoute] Guid groupId, [FromRoute] long userId, CancellationToken cancellationToken)
        {
            await _groupMemberRepository.ExecuteDeleteAsync(gm => gm.GroupId == groupId && gm.UserId == userId, cancellationToken);
            return Ok();
        }
    }
}
