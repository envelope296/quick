using Quick.MessagerBot.Base.Models.Users;

namespace Quick.MessagerBot.Base.Models.Messages
{
    /// <summary>
    /// Пересланное или ответное сообщение.
    /// </summary>
    public class LinkedMessage
    {
        /// <summary>
        /// Тип связанного сообщения.
        /// </summary>
        public MessageLinkType Type { get; set; }

        /// <summary>
        /// Пользователь, отправивший сообщение.
        /// </summary>
        public User? Sender { get; set; }

        /// <summary>
        /// Чат, в котором сообщение было изначально опубликовано. 
        /// Только для пересланных сообщений.
        /// </summary>
        public long ChatId { get; set; }

        /// <summary>
        /// Схема, представляющая тело сообщения.
        /// </summary>
        public MessageBody Message { get; set; }
    }
}
