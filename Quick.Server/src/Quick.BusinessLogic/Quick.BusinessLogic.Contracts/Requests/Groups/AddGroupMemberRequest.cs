namespace Quick.BusinessLogic.Contracts.Requests.Groups
{
    public class AddGroupMemberRequest
    {
        public required Guid GroupId { get; set; }

        public long UserId { get; set; }

        public Guid? SubgroupId { get; set; }
    }
}
