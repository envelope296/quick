using Quick.BusinessLogic.Contracts.Requests.Base;

namespace Quick.BusinessLogic.Contracts.Responses.Groups
{
    public class SearchGroupsRequest : BasePageRequest
    {
        public required string Query { get; set; }

        public required string University { get; set; }
    }
}
