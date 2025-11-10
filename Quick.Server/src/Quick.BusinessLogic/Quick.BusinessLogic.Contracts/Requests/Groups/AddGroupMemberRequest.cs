namespace Quick.BusinessLogic.Contracts.Requests.Groups
{
    public class AddGroupMemberRequest
    {
        public long UserId { get; set; }

        public Guid? SubgroupId { get; set; }
    }
}
