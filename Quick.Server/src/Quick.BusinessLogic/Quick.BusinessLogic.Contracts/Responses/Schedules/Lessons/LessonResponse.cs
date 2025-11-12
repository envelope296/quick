using Quick.BusinessLogic.Contracts.Responses.Subjects;
using Quick.BusinessLogic.Contracts.Responses.Teachers;

namespace Quick.BusinessLogic.Contracts.Responses.Schedules.Lessons
{
    public class LessonResponse
    {
        public required Guid Id { get; set; }

        public SubjectResponse? Subject { get; set; }

        public TeacherResponse? Teacher { get; set; }

        public LessonTypeResponse? LessonType { get; set; }

        public string? CabinetNumber { get; set; }

        public string? Address { get; set; }
    }
}
