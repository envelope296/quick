namespace Quick.BusinessLogic.Contracts.Responses.Groups
{
    public class GroupResponse
    {
        public required Guid Id { get; set; }

        public required string Name { get; set; }

        public string? Description { get; set; }

        public required long? OwnerId { get; set; }

        public required bool IsUserOwner { get; set; }

        public required bool IsPublic { get; set; }
    }
}
