using Quick.Common.DataAccess.Abstraction.Models;

namespace Quick.DataAccess.Models
{
    /// <summary>
    /// Группа.
    /// </summary>
    public class Group : BaseAuditableEntity<Guid>
    {
        /// <summary>
        /// Название группы.
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Признак: группа публичная.
        /// </summary>
        public bool IsPublic { get; set; }

        /// <summary>
        /// Идентификатор пользователя, которому принадлежит группа.
        /// </summary>
        public required long? OwnerId { get; set; }

        /// <summary>
        /// Пользователь, которому принадлежит группа.
        /// </summary>
        public User Owner { get; set; } = null!;

        /// <summary>
        /// Подгруппы группы.
        /// </summary>
        public ICollection<Subgroup> Subgroups { get; set; } = null!;

        /// <summary>
        /// Члены группы.
        /// </summary>
        public ICollection<GroupMember> Members { get; set; } = null!;

        /// <summary>
        /// Предметы группы.
        /// </summary>
        public ICollection<Subject> Subjects { get; set; } = null!;

        /// <summary>
        /// Расписания группы.
        /// </summary>
        public ICollection<Schedule> Schedules { get; set; } = null!;

        /// <summary>
        /// Преподаватели группы.
        /// </summary>
        public ICollection<Teacher> Teachers { get; set; } = null!;
    }
}