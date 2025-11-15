using Quick.DataAccess.Models;
using DayOfWeek = Quick.DataAccess.Models.DayOfWeek;

namespace Quick.BusinessLogic.Contracts.Requests.Schedules.Lessons
{
    public class AddLessonRequest
    {
        public required DayOfWeek DayOfWeek { get; set; }

        public WeekType? WeekType { get; set; }

        public Guid? SubgroupId { get; set; }

        public Guid SubjectId { get; set; }

        public Guid TimeSlotId { get; set; }

        public Guid? TeacherId { get; set; }

        public Guid? LessonTypeId { get; set; }

        public string? CabinetNumber { get; set; }

        public string? Address { get; set; }
    }
}
