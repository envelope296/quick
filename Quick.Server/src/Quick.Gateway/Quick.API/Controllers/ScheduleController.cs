using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quick.BusinessLogic.Contracts.Exceptions.Common;
using Quick.BusinessLogic.Contracts.Requests.Schedules;
using Quick.BusinessLogic.Contracts.Requests.Schedules.Lessons;
using Quick.BusinessLogic.Contracts.Responses.Common;
using Quick.BusinessLogic.Contracts.Responses.Schedules;
using Quick.BusinessLogic.Contracts.Responses.Schedules.Lessons;
using Quick.BusinessLogic.Contracts.Responses.Subjects;
using Quick.BusinessLogic.Contracts.Responses.Teachers;
using Quick.BusinessLogic.Repositories.Abstractions;
using Quick.DataAccess.Context.Extensions;
using Quick.DataAccess.Models;
using System.ComponentModel;
using System.Globalization;
using AppDayOfWeek = Quick.DataAccess.Models.DayOfWeek;
using SystemDayOfWeek = System.DayOfWeek;

namespace Quick.API.Controllers
{
    [ApiController]
    [Route("v1/schedules")]
    [Authorize]
    public class ScheduleController : ControllerBase
    {
        private static readonly CultureInfo RussianCulture = CultureInfo.GetCultureInfo("ru-RU");
        private static readonly Calendar Calendar = RussianCulture.Calendar;

        private readonly IScheduleRepository _scheduleRepository;
        private readonly ITimeSlotRepository _timeSlotRepository;
        private readonly ILessonRepository _lessonRepository;
        private readonly ILessonTypeRepository _lessonTypeRepository;

        public ScheduleController(
            IScheduleRepository scheduleRepository,
            ITimeSlotRepository timeSlotRepository,
            ILessonRepository lessonRepository,
            ILessonTypeRepository lessonTypeRepository)
        {
            _scheduleRepository = scheduleRepository;
            _timeSlotRepository = timeSlotRepository;
            _lessonRepository = lessonRepository;
            _lessonTypeRepository = lessonTypeRepository;
        }

        [HttpGet("{scheduleId:guid}")]
        public async Task<ActionResult<ScheduleResponse>> GetAsync([FromRoute] Guid scheduleId, CancellationToken cancellationToken)
        {
            // TODO: Validation

            var schedule = await _scheduleRepository
                .Filter(scheduleId)
                .Select(s => new ScheduleResponse
                {
                    Id = s.Id,
                    Name = s.Name,
                    Type = s.Type,
                })
                .FirstOrDefaultAsync(cancellationToken) ?? throw new EntityNotFoundException("Расписание не найдено");

            return Ok(schedule);
        }

        [HttpGet("page")]
        public async Task<ActionResult<PageResponse<ScheduleResponse>>> GetPageAsync(
            [FromQuery] Guid groupId,
            [FromQuery] int page,
            [FromQuery] int size, 
            CancellationToken cancellationToken)
        {
            // TODO: Validation

            var schedulesPage = await _scheduleRepository
                .Filter(s => s.GroupId == groupId)
                .OrderByDescending(s => s.CreatedOn)
                .Select(s => new ScheduleResponse
                {
                    Id = s.Id,
                    Name = s.Name,
                    Type = s.Type,
                })
                .ToPageResponseAsync(page, size, cancellationToken);

            return Ok(schedulesPage);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateAsync([FromBody] CreateScheduleRequest request, CancellationToken cancellationToken)
        {
            // TODO: Validation

            var schedule = new Schedule
            {
                GroupId = request.GroupId,
                Name = request.Name,
                Type = request.Type,
                LessonTypes = request.LessonTypes
                    .Select(lt =>
                    new LessonType
                    {
                        Name = lt.Name,
                        ShortName = lt.ShortName,
                        HexColor = lt.HexColor,
                    })
                    .ToList()
            };
            await _scheduleRepository.ExecuteAddAsync(schedule, cancellationToken);

            return Ok(schedule.Id);
        }

        [HttpPatch("{scheduleId:guid}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid scheduleId, [FromBody] object request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        [HttpPost("time-slots")]
        public async Task<ActionResult<Guid>> AddTimeSlotAsync([FromBody] AddTimeSlotRequest request, CancellationToken cancellationToken)
        {
            // TODO: Validation

            var timeSlot = new TimeSlot
            {
                ScheduleId = request.ScheduleId,
                From = request.From,
                To = request.To,
            };
            await _timeSlotRepository.ExecuteAddAsync(timeSlot, cancellationToken);

            return Ok(timeSlot.Id);
        }

        [HttpDelete("time-slots/{timeslotId:guid}")]
        public async Task<IActionResult> DeleteTimeSlotAsync([FromRoute] Guid timeslotId, CancellationToken cancellationToken)
        {
            // TODO: Validation

            await _timeSlotRepository.ExecuteDeleteAsync(timeslotId, cancellationToken);
            return Ok();
        }

        [HttpPost("time-slots/page/for-date")]
        public async Task<ActionResult<PageResponse<TimeSlotResponse>>> GetTimeSlotsPageForDateAsync([FromBody] GetTimeSlotsPageForDateRequest request, CancellationToken cancellationToken)
        {
            // TODO: Validation

            var scheduleType = await _scheduleRepository
                .Filter(request.ScheduleId)
                .Select<Schedule, ScheduleType?>(s => s.Type)
                .FirstOrDefaultAsync(cancellationToken);
            if (!scheduleType.HasValue)
            {
                return PageResponse<TimeSlotResponse>.Empty;
            }

            var dayOfWeek = GetDayOfWeekFromDate(request.Date);
            WeekType? weekType = scheduleType.Value == ScheduleType.Biweekly
                ? GetWeekTypeFromDate(request.Date)
                : null;

            var mappedRequest = new GetTimeSlotsPageForDayOfWeekRequest
            {
                ScheduleId = request.ScheduleId,
                SubgroupId = request.SubgroupId,
                DayOfWeek = dayOfWeek,
                WeekType = weekType,
                Page = request.Page,
                Size = request.Size
            };
            var result = await GetTimeSlotsPageAsync(mappedRequest, cancellationToken);
            
            return result;
        }

        [HttpPost("time-slots/page/for-day-of-week")]
        public async Task<ActionResult<PageResponse<TimeSlotResponse>>> GetTimeSlotsPageForDayOfWeekAsync([FromBody] GetTimeSlotsPageForDayOfWeekRequest request, CancellationToken cancellationToken)
        {
            // TODO: Validation

            var result = await GetTimeSlotsPageAsync(request, cancellationToken);
            return Ok(result);
        }

        [HttpPost("lessons")]
        public async Task<ActionResult<Guid>> AddLessonAsync([FromBody] AddLessonRequest request, CancellationToken cancellationToken)
        {
            // TODO: Validation

            var lesson = new Lesson
            {
                SubjectId = request.SubjectId,
                TimeSlotId = request.TimeSlotId,
                DayOfWeek = request.DayOfWeek,
                SubgroupId = request.SubgroupId,
                WeekType = request.WeekType,
                TeacherId = request.TeacherId,
                CabinetNumber = request.CabinetNumber,
                LessonTypeId = request.LessonTypeId,
                Address = request.Address,
            };
            await _lessonRepository.ExecuteAddAsync(lesson, cancellationToken);

            return Ok(lesson.Id);
        }

        [HttpDelete("lessons/{lessonId:guid}")]
        public async Task<ActionResult<Guid>> DeleteLessonAsync([FromRoute] Guid lessonId, CancellationToken cancellationToken)
        {
            // TODO: Validation

            await _lessonRepository.ExecuteDeleteAsync(lessonId, cancellationToken);
            return Ok(lessonId);
        }

        [HttpGet("{scheduleId:guid}/lesson-types/page")]
        public async Task<ActionResult<PageResponse<LessonTypeResponse>>> GetLessonTypesPageAsync(
            [FromRoute] Guid scheduleId,
            [FromQuery] int page,
            [FromQuery] int size,
            CancellationToken cancellationToken)
        {
            // TODO: Validation

            var lessonTypesPage = await _lessonTypeRepository
                .Filter(lt => lt.ScheduleId == scheduleId)
                .OrderBy(lt => lt.Name)
                .Select(lt => new LessonTypeResponse
                {
                    Id = lt.Id,
                    Name = lt.Name,
                    ShortName = lt.ShortName,
                    HexColor = lt.HexColor,
                })
                .ToPageResponseAsync(page, size, cancellationToken);

            return Ok(lessonTypesPage);
        }

        private async Task<PageResponse<TimeSlotResponse>> GetTimeSlotsPageAsync(GetTimeSlotsPageForDayOfWeekRequest request, CancellationToken cancellationToken)
        {
            var timeSlotsPage = await _timeSlotRepository
                .Filter(ts => ts.ScheduleId == request.ScheduleId)
                .Select(ts => new TimeSlotResponse
                {
                    Id = ts.Id,
                    From = ts.From,
                    To = ts.To
                })
                .ToPageResponseAsync(request, cancellationToken);
            var timeSlotsIds = timeSlotsPage.Items.Select(t => t.Id).ToList();

            var lessonsQuery = _lessonRepository.Filter(l =>
                timeSlotsIds.Contains(l.TimeSlotId) &&
                l.DayOfWeek == request.DayOfWeek);

            if (request.WeekType.HasValue)
            {
                lessonsQuery = lessonsQuery.Where(l => l.WeekType == request.WeekType.Value || l.WeekType == null);
            }
            if (request.SubgroupId.HasValue)
            {
                lessonsQuery = lessonsQuery.Where(l => l.SubgroupId == request.SubgroupId.Value);
            }

            var lessonsByTimeSlotIds = await lessonsQuery
                .Include(l => l.Subject)
                .Include(l => l.Teacher)
                .Include(l => l.LessonType)
                .GroupBy(l => l.TimeSlotId)
                .ToDictionaryAsync(
                    g => g.Key,
                    g => g.Select(l => new LessonResponse
                    {
                        Id = l.Id,
                        Subject = l.Subject != null ? new SubjectResponse
                        {
                            Id = l.Subject.Id,
                            Name = l.Subject.Name,
                        } : null,
                        Teacher = l.Teacher != null ? new TeacherResponse
                        {
                            Id = l.Teacher.Id,
                            FullName = l.Teacher.FullName,
                            Email = l.Teacher.Email,
                            PhoneNumber = l.Teacher.PhoneNumber,
                            MessagerUserId = l.Teacher.MessagerUserId,
                        } : null,
                        LessonType = l.LessonType != null ? new LessonTypeResponse {
                            Id = l.LessonType.Id,
                            Name = l.LessonType.Name,
                            ShortName = l.LessonType.ShortName,
                            HexColor = l.LessonType.HexColor,
                        } : null,
                        CabinetNumber = l.CabinetNumber,
                        Address = l.Address,
                    }).ToList());

            foreach (var timeSlot in timeSlotsPage.Items)
            {
                if (lessonsByTimeSlotIds.TryGetValue(timeSlot.Id, out var lessons))
                {
                    timeSlot.Lessons = lessons;
                }
            }

            return timeSlotsPage;
        }

        private static AppDayOfWeek GetDayOfWeekFromDate(DateTime date)
        {
            return date.DayOfWeek switch
            {
                SystemDayOfWeek.Monday => AppDayOfWeek.Monday,
                SystemDayOfWeek.Tuesday => AppDayOfWeek.Tuesday,
                SystemDayOfWeek.Wednesday => AppDayOfWeek.Wednesday,
                SystemDayOfWeek.Thursday => AppDayOfWeek.Thursday,
                SystemDayOfWeek.Friday => AppDayOfWeek.Friday,
                SystemDayOfWeek.Saturday => AppDayOfWeek.Saturday,
                SystemDayOfWeek.Sunday => AppDayOfWeek.Sunday,
                _ => throw new InvalidEnumArgumentException()
            };
        }

        private static WeekType GetWeekTypeFromDate(DateTime date)
        {
            int weekNumber = Calendar.GetWeekOfYear(
                date,
                CalendarWeekRule.FirstFourDayWeek,
                SystemDayOfWeek.Monday);

            return weekNumber % 2 == 0
                ? WeekType.Even
                : WeekType.Odd;
        }
    }
}
