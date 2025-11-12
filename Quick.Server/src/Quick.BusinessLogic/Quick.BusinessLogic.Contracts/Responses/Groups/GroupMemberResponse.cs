namespace Quick.BusinessLogic.Contracts.Responses.Groups
{
    public class GroupMemberResponse
    {
        public required long UserId { get; set; }

        public required long MessagerUserId { get; set; }

        public required string FirstName { get; set; }

        public string? LastName { get; set; }

        public string? UserName { get; set; }

        public string? PhotoUrl { get; set; }
    }
}
