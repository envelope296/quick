using Quick.Common.DataAccess.Abstraction.Models;

namespace Quick.DataAccess.Models
{
    /// <summary>
    /// Занятие.
    /// </summary>
    public class Lesson : BaseEntity<Guid>
    {
        /// <summary>
        /// Идентификатор предмета.
        /// </summary>
        public required Guid SubjectId { get; set; }

        /// <summary>
        /// Идентификатор временного слота.
        /// </summary>
        public required Guid TimeSlotId { get; set; }

        /// <summary>
        /// День недели.
        /// </summary>
        public required DayOfWeek DayOfWeek { get; set; }

        /// <summary>
        /// Идентификатор подгруппы.
        /// </summary>
        public Guid? SubgroupId { get; set; }

        /// <summary>
        /// Тип недели.
        /// </summary>
        public WeekType? WeekType { get; set; }

        /// <summary>
        /// Идентификатор преподавателя.
        /// </summary>
        public Guid? TeacherId { get; set; }

        /// <summary>
        /// Идентификатор типа занятия.
        /// </summary>
        public Guid? LessonTypeId { get; set; }

        /// <summary>
        /// Предмет.
        /// </summary>
        public Subject Subject { get; set; } = null!;

        /// <summary>
        /// Временной слот.
        /// </summary>
        public TimeSlot TimeSlot { get; set; } = null!;

        /// <summary>
        /// Подгруппа.
        /// </summary>
        public Subgroup? Subgroup { get; set; }

        /// <summary>
        /// Преподаватель.
        /// </summary>
        public Teacher? Teacher { get; set; }

        /// <summary>
        /// Тип занятия.
        /// </summary>
        public LessonType? LessonType { get; set; }
    }
}