namespace Quick.BusinessLogic.Contracts.Requests.Subjects
{
    public class CreateSubjectRequest
    {
        public required Guid GroupId { get; set; }

        public required string Name { get; set; }
    }
}
