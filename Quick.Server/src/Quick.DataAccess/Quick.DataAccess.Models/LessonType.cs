using Quick.Common.DataAccess.Abstraction.Models;

namespace Quick.DataAccess.Models
{
    /// <summary>
    /// Тип занятия.
    /// </summary>
    public class LessonType : BaseEntity<Guid>
    {
        /// <summary>
        /// Наименование типа занятия.
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Цвет выделения типа занятия.
        /// </summary>
        public string? HexColor { get; set; }

        /// <summary>
        /// Идентификатор расписания.
        /// </summary>
        public required Guid ScheduleId { get; set; }

        /// <summary>
        /// Расписание.
        /// </summary>
        public Schedule Schedule { get; set; } = null!;

        /// <summary>
        /// Занятия этого типа.
        /// </summary>
        public ICollection<Lesson> Lessons { get; set; } = null!;
    }
}