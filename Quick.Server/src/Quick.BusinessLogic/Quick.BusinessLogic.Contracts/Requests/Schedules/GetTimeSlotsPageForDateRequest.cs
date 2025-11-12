namespace Quick.BusinessLogic.Contracts.Requests.Schedules
{
    public class GetTimeSlotsPageForDateRequest : GetTimeSlotsPageRequestBase
    {
        public required DateTime Date { get; set; }
    }
}
