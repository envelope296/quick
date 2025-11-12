using Quick.Common.DataAccess.Abstraction.Models;

namespace Quick.DataAccess.Models
{
    /// <summary>
    /// Временной слот.
    /// </summary>
    public class TimeSlot : BaseEntity<Guid>
    {
        public int? OrderNumber { get; set; }

        /// <summary>
        /// Начало.
        /// </summary>
        public TimeSpan? From { get; set; }

        /// <summary>
        /// Конец.
        /// </summary>
        public TimeSpan? To { get; set; }

        /// <summary>
        /// Идентификатор расписания.
        /// </summary>
        public required Guid ScheduleId { get; set; }

        /// <summary>
        /// Расписание.
        /// </summary>
        public Schedule Schedule { get; set; } = null!;

        /// <summary>
        /// Занятия слота.
        /// </summary>
        public ICollection<Lesson> Lessons { get; set; } = null!;
    }
}