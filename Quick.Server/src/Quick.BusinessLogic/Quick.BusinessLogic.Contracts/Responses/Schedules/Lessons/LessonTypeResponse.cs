namespace Quick.BusinessLogic.Contracts.Responses.Schedules.Lessons
{
    public class LessonTypeResponse
    {
        public required Guid Id { get; set; }
        
        public required string Name { get; set; }

        public string? ShortName { get; set; }

        public string? HexColor { get; set; }
    }
}
