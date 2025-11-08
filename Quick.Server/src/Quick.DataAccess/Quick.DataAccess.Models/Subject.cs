using Quick.Common.DataAccess.Abstraction.Models;

namespace Quick.DataAccess.Models
{
    /// <summary>
    /// Предмет.
    /// </summary>
    public class Subject : BaseEntity<Guid>
    {
        /// <summary>
        /// Наименование предмета.
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Идентификатор группы.
        /// </summary>
        public required Guid GroupId { get; set; }

        /// <summary>
        /// Группа.
        /// </summary>
        public Group Group { get; set; } = null!;

        /// <summary>
        /// Занятия по предмету.
        /// </summary>
        public ICollection<Lesson> Lessons { get; set; } = null!;
    }
}