using Quick.MessagerBot.Base.Models.Chats;
using Quick.MessagerBot.Base.Models.Users;

namespace Quick.MessagerBot.Base.Models.Messages
{
    /// <summary>
    /// Сообщение в чате.
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Пользователь, отправивший сообщение.
        /// </summary>
        public User? Sender { get; set; }

        /// <summary>
        /// Получатель сообщения. Может быть пользователем или чатом.
        /// </summary>
        public Recipient Recipient { get; set; }

        /// <summary>
        /// Время создания сообщения в формате Unix-time.
        /// </summary>
        public long Timestamp { get; set; }

        //public LinkedMessage? LinkedMessage { get; set; }

        /// <summary>
        /// Содержимое сообщения. 
        /// Может быть null, если сообщение содержит только пересланное сообщение.
        /// </summary>
        public MessageBody Body { get; set; }

        //public MessageStat? Stat { get; set; }

        /// <summary>
        /// Публичная ссылка на сообщение. 
        /// Может быть null для диалогов или не публичных чатов.
        /// </summary>
        public string? Url { get; set; }
    }
}
