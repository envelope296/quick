using Quick.DataAccess.Models;

namespace Quick.BusinessLogic.Contracts.Responses.Schedules
{
    public class ScheduleResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ScheduleType Type { get; set; }
    }
}
