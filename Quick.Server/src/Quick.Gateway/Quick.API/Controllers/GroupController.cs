using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Quick.BusinessLogic.Contracts.Exceptions.Common;
using Quick.BusinessLogic.Contracts.Requests.Groups;
using Quick.BusinessLogic.Contracts.Responses.Common;
using Quick.BusinessLogic.Contracts.Responses.Groups;
using Quick.BusinessLogic.Repositories.Abstractions;
using Quick.Common.Context.UserContext;
using Quick.DataAccess.Context.Extensions;
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

        [HttpGet("{groupId:guid}")]
        public async Task<ActionResult<GroupResponse>> GetAsync([FromRoute] Guid groupId, CancellationToken cancellationToken)
        {
            var userContext = _userContextAccessor.GetCurrentOrFail();

            // TODO: Validation

            var group = await _groupRepository
                .Filter(groupId)
                .Select(g => new GroupResponse
                {
                    Id = g.Id,
                    Name = g.Name,
                    OwnerId = g.OwnerId,
                    IsUserOwner = g.OwnerId == userContext.UserId,
                    IsPublic = g.IsPublic,
                })
                .FirstOrDefaultAsync(cancellationToken) ?? throw new EntityNotFoundException("Группа не найдена");

            return Ok(group);
        }

        [HttpGet("page")]
        public async Task<ActionResult<PageResponse<GroupResponse>>> GetPageAsync(
            [FromQuery] int page, 
            [FromQuery] int size, 
            CancellationToken cancellationToken)
        {
            var userContext = _userContextAccessor.GetCurrentOrFail();

            // TODO: Validation

            var groupsPage = await _groupRepository.Filter(g =>
                g.OwnerId == userContext.UserId ||
                g.Members.Any(gm => gm.UserId == userContext.UserId)
            )
            .OrderByDescending(g => g.IsPublic)
            .ThenBy(g => g.CreatedOn)
            .Select(g => new GroupResponse
            {
                Id = g.Id,
                Name = g.Name,
                OwnerId = g.OwnerId,
                IsUserOwner = g.OwnerId == userContext.UserId,
                IsPublic = g.IsPublic,
            })
            .ToPageResponseAsync(page, size, cancellationToken);
            
            return Ok(groupsPage);
        }

        [HttpPost("search")]
        public async Task<ActionResult<PageResponse<GroupResponse>>> SearchAsync(
            [FromBody] SearchGroupsRequest request, 
            CancellationToken cancellationToken)
        {
            var userContext = _userContextAccessor.GetCurrentOrFail();

            var groupsPage = await _groupRepository.Filter(g =>
                g.IsPublic &&
                g.OwnerId != userContext.UserId &&
                g.Members.All(gm => gm.UserId != userContext.UserId) &&
                g.University == request.University &&
                g.Name.Contains(request.Query))
            .OrderBy(g => g.CreatedOn)
            .Select(g => new GroupResponse
            {
                Id = g.Id,
                Name = g.Name,
                OwnerId = g.OwnerId,
                IsUserOwner = g.OwnerId == userContext.UserId,
                IsPublic = g.IsPublic,
            }).ToPageResponseAsync(request, cancellationToken);

            return Ok(groupsPage);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateAsync([FromBody] CreateGroupRequest request, CancellationToken cancellationToken)
        {
            var userContext = _userContextAccessor.GetCurrentOrFail();
            
            var group = new Group
            {
                Name = request.Name,
                University = request.University,
                OwnerId =  userContext.UserId,
                IsPublic = true,
                Subgroups = request.Subgroups.Select(name => 
                    new Subgroup
                    {
                        Name = name
                    }).ToList()
            };
            await _groupRepository.ExecuteAddAsync(group, cancellationToken);
            
            return Ok(group.Id);
        }

        [HttpGet("{groupId:guid}/members/page")]
        public async Task<ActionResult<PageResponse<GroupMemberResponse>>> GetMembersPageAsync(
            [FromRoute] Guid groupId, 
            [FromQuery] int page, 
            [FromQuery] int size, 
            CancellationToken cancellationToken)
        {
            // TODO: Validation

            var groupMembers = await _groupMemberRepository
                .Filter(gm => gm.GroupId == groupId)
                .Select(gm => new GroupMemberResponse
                {
                    UserId = gm.UserId,
                    MessagerUserId = gm.User.MessagerUserId,
                    FirstName = gm.User.FirstName,
                    LastName = gm.User.LastName,
                    UserName = gm.User.UserName,
                    PhotoUrl = gm.User.PhotoUrl,
                })
                .ToPageResponseAsync(page, size, cancellationToken);
            
            return Ok(groupMembers);
        }

        [HttpGet("{groupId:guid}/join")]
        public async Task<ActionResult<JoinGroupResponse>> JoinAsync(
            [FromRoute] Guid groupId,
            CancellationToken cancellationToken)
        {
            var userContext = _userContextAccessor.GetCurrentOrFail();

            var isOwnerOrMember = await _groupRepository.CheckIfExistsAsync(g => 
                g.Id == groupId && 
                (g.OwnerId == userContext.UserId || g.Members.Any(gm => gm.UserId == userContext.UserId)),
            cancellationToken);

            if (isOwnerOrMember)
            {
                return Ok(JoinGroupResponse.Error(groupId, "Вы уже состоите в группе"));
            }

            var groupMember = new GroupMember
            {
                GroupId = groupId,
                UserId = userContext.UserId
            };
            await _groupMemberRepository.ExecuteAddAsync(groupMember, cancellationToken);

            return Ok(JoinGroupResponse.Success(groupId));
        }

        [HttpPost("members")]
        public async Task<ActionResult> AddMemberAsync([FromBody] AddGroupMemberRequest request, CancellationToken cancellationToken)
        {
            // TODO: Validation

            var groupMember = new GroupMember
            {
                GroupId = request.GroupId,
                UserId = request.UserId,
                SubgroupId = request.SubgroupId
            };
            await _groupMemberRepository.ExecuteAddAsync(groupMember, cancellationToken);
            return Ok();
        }

        [HttpDelete("{groupId:guid}/members/{userId:long}")]
        public async Task<IActionResult> DeleteMemberAsync([FromRoute] Guid groupId, [FromRoute] long userId, CancellationToken cancellationToken)
        {
            // TODO: Validation

            await _groupMemberRepository.ExecuteDeleteAsync(gm => gm.GroupId == groupId && gm.UserId == userId, cancellationToken);
            return Ok();
        }
    }
}
