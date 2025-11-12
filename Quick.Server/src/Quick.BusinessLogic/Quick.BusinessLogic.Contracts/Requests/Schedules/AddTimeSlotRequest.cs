namespace Quick.BusinessLogic.Contracts.Requests.Schedules
{
    public class AddTimeSlotRequest
    {
        public required Guid ScheduleId { get; set; }

        public required string Name { get; set; }

        public required TimeSpan From { get; set; }

        public required TimeSpan To { get; set; }
    }
}
