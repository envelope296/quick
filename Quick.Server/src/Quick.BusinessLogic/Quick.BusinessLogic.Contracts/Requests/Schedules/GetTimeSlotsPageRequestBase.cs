using Quick.BusinessLogic.Contracts.Requests.Base;

namespace Quick.BusinessLogic.Contracts.Requests.Schedules
{
    public abstract class GetTimeSlotsPageRequestBase : BasePageRequest
    {
        public required Guid ScheduleId { get; set; }

        public Guid? SubgroupId { get; set; }
    }
}
