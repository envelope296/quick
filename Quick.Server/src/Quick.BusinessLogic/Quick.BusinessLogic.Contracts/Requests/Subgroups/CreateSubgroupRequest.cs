namespace Quick.BusinessLogic.Contracts.Requests.Subgroups
{
    public class CreateSubgroupRequest
    {
        public required Guid GroupId { get; set; }

        public required string Name { get; set; }
    }
}
