namespace Quick.BusinessLogic.Contracts.Requests.Schedules.Lessons
{
    public class LessonTypeModel
    {
        public required string Name { get; set; }

        public string? ShortName { get; set; }

        public string? HexColor { get; set; }
    }
}
