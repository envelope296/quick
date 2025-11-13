namespace Quick.BusinessLogic.Contracts.Requests.Groups
{
    public class CreateGroupRequest
    {
        public required string Name { get; set; }

        public string? University { get; set; }

        public List<string> Subroups { get; set; }
    }
}
