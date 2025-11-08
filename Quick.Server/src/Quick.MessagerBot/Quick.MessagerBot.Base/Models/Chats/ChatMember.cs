using Quick.MessagerBot.Base.Models.Users;

namespace Quick.MessagerBot.Base.Models.Chats
{
    /// <summary>
    /// Объект, описывающий участника чата.
    /// </summary>
    public class ChatMember : UserWithPhoto
    {
        /// <summary>
        /// Время последней активности пользователя в чате. 
        /// Может быть устаревшим для суперчатов (равно времени вступления).
        /// </summary>
        public long LastAccessTime { get; set; }

        /// <summary>
        /// Является ли пользователь владельцем чата.
        /// </summary>
        public bool IsOwner { get; set; }

        /// <summary>
        /// Является ли пользователь администратором чата.
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// Дата присоединения к чату в формате Unix time.
        /// </summary>
        public long JoinTime { get; set; }

        /// <summary>
        /// Перечень прав пользователя.
        /// </summary>
        public ChatAdminPermission[]? Permissions { get; set; }

        /// <summary>
        /// Заголовок, который будет показан на клиенте.
        /// </summary>
        public string? Alias { get; set; }
    }
}
