using Quick.BusinessLogic.Contracts.Requests.Schedules.Lessons;
using Quick.DataAccess.Models;

namespace Quick.BusinessLogic.Contracts.Requests.Schedules
{
    public class CreateScheduleRequest
    {
        public required Guid GroupId { get; set; }

        public required string Name { get; set; }

        public required ScheduleType Type { get; set; }

        public List<LessonTypeModel> LessonTypes { get; set; } = new List<LessonTypeModel>();
    }
}
