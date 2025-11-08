using Quick.Common.DataAccess.Abstraction.Models;

namespace Quick.DataAccess.Models
{
    /// <summary>
    /// Подгруппа.
    /// </summary>
    public class Subgroup : BaseEntity<Guid>
    {
        /// <summary>
        /// Название подгруппы.
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
        /// Члены подгруппы.
        /// </summary>
        public ICollection<GroupMember> GroupMembers { get; set; } = null!;

        /// <summary>
        /// Занятия подгруппы.
        /// </summary>
        public ICollection<Lesson> Lessons { get; set; } = null!;
    }
}