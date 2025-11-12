using Quick.BusinessLogic.Contracts.Responses.Schedules.Lessons;

namespace Quick.BusinessLogic.Contracts.Responses.Schedules
{
    public class TimeSlotResponse
    {
        public required Guid Id { get; set; }

        public TimeSpan? From { get; set; }

        public TimeSpan? To { get; set; }

        public List<LessonResponse> Lessons { get; set; } = new List<LessonResponse>();
    }
}
