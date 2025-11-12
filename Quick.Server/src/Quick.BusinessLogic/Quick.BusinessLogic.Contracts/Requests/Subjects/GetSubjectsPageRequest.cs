using Quick.BusinessLogic.Contracts.Requests.Base;


namespace Quick.BusinessLogic.Contracts.Requests.Subjects
{
    public class GetSubjectsPageRequest : BasePageRequest
    {
        public required Guid GroupId { get; set; }

        public string? SearchText { get; set; }
    }
}
