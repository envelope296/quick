namespace Quick.BusinessLogic.Contracts.Requests.Teachers
{
    public class CreateTeacherRequest
    {
        public required Guid GroupId { get; set; }

        public required string FullName { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public long? MessagerUserId { get; set; }
    }
}
