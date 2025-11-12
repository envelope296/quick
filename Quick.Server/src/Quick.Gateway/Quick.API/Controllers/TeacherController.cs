using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quick.BusinessLogic.Contracts.Requests.Teachers;
using Quick.BusinessLogic.Contracts.Responses.Common;
using Quick.BusinessLogic.Contracts.Responses.Teachers;
using Quick.BusinessLogic.Repositories.Abstractions;
using Quick.BusinessLogic.Repositories.Implementations;
using Quick.DataAccess.Context.Extensions;
using Quick.DataAccess.Models;

namespace Quick.API.Controllers
{
    [ApiController]
    [Route("v1/teachers")]
    [Authorize]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherRepository _teacherRepository;

        public TeacherController(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        [HttpPost("page")]
        public async Task<ActionResult<PageResponse<TeacherResponse>>> GetPageAsync([FromBody] GetTeachersPageRequest request, CancellationToken cancellationToken)
        {
            // TODO: Validation

            var query = _teacherRepository.Filter(s => s.GroupId == request.GroupId);
            if (!string.IsNullOrWhiteSpace(request.SearchText))
            {
                var searchText = request.SearchText.ToLower().Trim();
                query = query.Where(s => s.FullName.ToLower().Contains(searchText));
            }

            var teachersPage = await query
                .OrderBy(t => t.FullName)
                .Select(t => new TeacherResponse
                {
                    Id = t.Id,
                    FullName = t.FullName,
                    Email = t.Email,
                    PhoneNumber = t.PhoneNumber,
                    MessagerUserId = t.MessagerUserId,
                })
                .ToPageResponseAsync(request, cancellationToken);

            return Ok(teachersPage);
        }

        [HttpDelete("{teacherId:guid}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid teacherId, CancellationToken cancellationToken)
        {
            // TODO: Validation

            await _teacherRepository.ExecuteDeleteAsync(teacherId, cancellationToken);
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateAsync([FromBody] CreateTeacherRequest request, CancellationToken cancellationToken)
        {
            // TODO: Validation

            var teacher = new Teacher
            {
                GroupId = request.GroupId,
                FullName = request.FullName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                MessagerUserId = request.MessagerUserId,
            };
            await _teacherRepository.ExecuteAddAsync(teacher, cancellationToken);

            return Ok(teacher.Id);
        }
    }
}
