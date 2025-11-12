using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quick.BusinessLogic.Contracts.Requests.Subjects;
using Quick.BusinessLogic.Contracts.Responses.Common;
using Quick.BusinessLogic.Contracts.Responses.Subjects;
using Quick.BusinessLogic.Repositories.Abstractions;
using Quick.DataAccess.Context.Extensions;
using Quick.DataAccess.Models;

namespace Quick.API.Controllers
{
    [ApiController]
    [Route("v1/subjects")]
    [Authorize]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectRepository _subjectRepository;

        public SubjectController(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        [HttpPost("page")]
        public async Task<ActionResult<PageResponse<SubjectResponse>>> GetPageAsync([FromBody] GetSubjectsPageRequest request, CancellationToken cancellationToken)
        {
            // TODO: Validation

            var query = _subjectRepository.Filter(s => s.GroupId == request.GroupId);
            if (!string.IsNullOrWhiteSpace(request.SearchText))
            {
                var searchText = request.SearchText.ToLower().Trim();
                query = query.Where(s => s.Name.ToLower().Contains(searchText));
            }

            var subjectsPage = await query
                .OrderBy(s => s.Name)
                .Select(s => new SubjectResponse
                {
                    Id = s.Id,
                    Name = s.Name,
                })
                .ToPageResponseAsync(request, cancellationToken);

            return Ok(subjectsPage);
        }

        [HttpDelete("{subjectId:guid}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid subjectId, CancellationToken cancellationToken)
        {
            // TODO: Validation

            await _subjectRepository.ExecuteDeleteAsync(subjectId, cancellationToken);
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateAsync([FromBody] CreateSubjectRequest request, CancellationToken cancellationToken)
        {
            // TODO: Validation

            var subject = new Subject
            {
                GroupId = request.GroupId,
                Name = request.Name,
            };
            await _subjectRepository.ExecuteAddAsync(subject, cancellationToken);

            return Ok(subject.Id);
        }
    }
}
