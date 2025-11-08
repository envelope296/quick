using Quick.Common.DataAccess.Abstraction.Models;

namespace Quick.DataAccess.Models
{
    /// <summary>
    /// Член группы.
    /// </summary>
    public class GroupMember : IAuditableEntity
    {
        /// <summary>
        /// Идентификатор группы.
        /// </summary>
        public required Guid GroupId { get; set; }

        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public required long UserId { get; set; }

        /// <summary>
        /// Идентификатор подгруппы.
        /// </summary>
        public Guid? SubgroupId { get; set; }

        /// <inheritdoc/>
        public required DateTimeOffset CreatedOn { get; set; }

        /// <inheritdoc/>
        public DateTimeOffset? UpdatedOn { get; set; }

        /// <summary>
        /// Группа.
        /// </summary>
        public Group Group { get; set; } = null!;

        /// <summary>
        /// Пользователь.
        /// </summary>
        public User User { get; set; } = null!;

        /// <summary>
        /// Подгруппа.
        /// </summary>
        public Subgroup? Subgroup { get; set; }
    }
}