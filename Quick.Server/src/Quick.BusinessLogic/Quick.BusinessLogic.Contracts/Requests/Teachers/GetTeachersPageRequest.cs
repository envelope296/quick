using Quick.BusinessLogic.Contracts.Requests.Base;

namespace Quick.BusinessLogic.Contracts.Requests.Teachers
{
    public class GetTeachersPageRequest : BasePageRequest
    {
        public required Guid GroupId { get; set; }

        public string? SearchText { get; set; }
    }
}
