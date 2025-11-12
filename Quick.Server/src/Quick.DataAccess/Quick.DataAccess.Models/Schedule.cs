using Quick.Common.DataAccess.Abstraction.Models;

namespace Quick.DataAccess.Models
{
    /// <summary>
    /// Расписание.
    /// </summary>
    public class Schedule : BaseAuditableEntity<Guid>
    {
        /// <summary>
        /// Наименование расписания.
        /// </summary>
        public required string Name { get; set; }

        public required ScheduleType Type { get; set; }

        /// <summary>
        /// Идентификатор группы.
        /// </summary>
        public required Guid GroupId { get; set; }

        /// <summary>
        /// Группа.
        /// </summary>
        public Group Group { get; set; } = null!;

        /// <summary>
        /// Типы занятий.
        /// </summary>
        public ICollection<LessonType> LessonTypes { get; set; } = null!;

        /// <summary>
        /// Временные слоты.
        /// </summary>
        public ICollection<TimeSlot> TimeSlots { get; set; } = null!;
    }
}