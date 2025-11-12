namespace Quick.BusinessLogic.Contracts.Responses.Teachers
{
    public class TeacherResponse
    {
        public required Guid Id { get; set; }

        public required string FullName { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public long? MessagerUserId { get; set; }
    }
}
