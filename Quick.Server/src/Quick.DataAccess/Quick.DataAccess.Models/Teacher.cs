using Quick.Common.DataAccess.Abstraction.Models;

namespace Quick.DataAccess.Models
{
    /// <summary>
    /// Преподаватель.
    /// </summary>
    public class Teacher : BaseAuditableEntity<Guid>
    {
        /// <summary>
        /// Идентификатор группы.
        /// </summary>
        public required Guid GroupId { get; set; }

        /// <summary>
        /// Полное имя преподавателя.
        /// </summary>
        public required string FullName { get; set; }

        /// <summary>
        /// Электронная почта.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Номер телефона.
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Идентификатор пользователя в мессенджере.
        /// </summary>
        public long? MessagerUserId { get; set; }

        /// <summary>
        /// Группа.
        /// </summary>
        public Group Group { get; set; } = null!;

        /// <summary>
        /// Занятия.
        /// </summary>
        public ICollection<Lesson> Lessons { get; set; } = null!;
    }
}