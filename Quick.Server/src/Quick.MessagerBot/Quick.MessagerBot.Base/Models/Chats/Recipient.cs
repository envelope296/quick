namespace Quick.MessagerBot.Base.Models.Chats
{
    /// <summary>
    /// Получатель сообщения. Может быть пользователем или чатом.
    /// </summary>
    public class Recipient
    {
        /// <summary>
        /// ID чата.
        /// </summary>
        public long? ChatId { get; set; }

        /// <summary>
        /// Тип чата.
        /// </summary>
        public ChatType ChatType { get; set; }

        /// <summary>
        /// ID пользователя, если сообщение было отправлено пользователю.
        /// </summary>
        public long? UserId { get; set; }
    }
}
