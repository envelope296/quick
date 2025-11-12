using Quick.DataAccess.Models;
using DayOfWeek = Quick.DataAccess.Models.DayOfWeek;

namespace Quick.BusinessLogic.Contracts.Requests.Schedules
{
    public class GetTimeSlotsPageForDayOfWeekRequest : GetTimeSlotsPageRequestBase
    {
        public required DayOfWeek DayOfWeek { get; set; }

        public WeekType? WeekType { get; set; }
    }
}
