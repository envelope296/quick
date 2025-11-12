using Quick.Common.DataAccess.Abstraction.Models;

namespace Quick.DataAccess.Models
{
    /// <summary>
    /// Пользователь.
    /// </summary>
    public class User : BaseAuditableEntity<long>
    {
        /// <summary>
        /// Идентификатор пользователя в мессенджере.
        /// </summary>
        public required long MessagerUserId { get; set; }

        public required string FirstName { get; set; }

        public string? LastName { get; set; }

        public string? UserName { get; set; }

        public string? PhotoUrl { get; set; }

        /// <summary>
        /// Идентификатор текущего расписания.
        /// </summary>
        public Guid? CurrentScheduleId { get; set; }

        /// <summary>
        /// Текущее расписание.
        /// </summary>
        public Schedule? CurrentSchedule { get; set; }

        /// <summary>
        /// Группы, принадлежащие пользователю.
        /// </summary>
        public ICollection<Group> Groups { get; set; } = null!;

        /// <summary>
        /// Членства пользователя в группах.
        /// </summary>
        public ICollection<GroupMember> GroupMembers { get; set; } = null!;
    }
}