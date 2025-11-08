using Quick.MessagerBot.Base.Models.Attachments;
using Quick.MessagerBot.Base.Models.Users;
using Quick.MessagerBot.Base.Models.Messages;

namespace Quick.MessagerBot.Base.Models.Chats
{
    /// <summary>
    /// Объект чата.
    /// </summary>
    public class Chat
    {
        /// <summary>
        /// ID чата.
        /// </summary>
        public long ChatId { get; set; }

        /// <summary>
        /// Тип чата.
        /// </summary>
        public ChatType Type { get; set; }

        /// <summary>
        /// Статус чата.
        /// </summary>
        public ChatStatus Status { get; set; }

        /// <summary>
        /// Отображаемое название чата. 
        /// Может быть null для диалогов.
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Иконка чата.
        /// </summary>
        public Image? Icon { get; set; }

        /// <summary>
        /// Время последнего события в чате.
        /// </summary>
        public long LastEventTime { get; set; }

        /// <summary>
        /// Количество участников чата. 
        /// Для диалогов всегда 2.
        /// </summary>
        public int ParticipantsCount { get; set; }

        /// <summary>
        /// ID владельца чата.
        /// </summary>
        public long? OwnerId { get; set; }

        /// <summary>
        /// Участники чата с временем последней активности.
        /// Может быть null, если запрашивается список чатов.
        /// </summary>
        public ChatMember[]? Participants { get; set; }

        /// <summary>
        /// Доступен ли чат публично (для диалогов всегда false).
        /// </summary>
        public bool IsPublic { get; set; }

        /// <summary>
        /// Ссылка на чат.
        /// </summary>
        public string? Link { get; set; }

        /// <summary>
        /// Описание чата.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Данные о пользователе в диалоге (только для чатов типа "dialog").
        /// </summary>
        public UserWithPhoto? DialogWithUser { get; set; }

        /// <summary>
        /// ID сообщения, содержащего кнопку, через которую был инициирован чат.
        /// </summary>
        public string? ChatMessageId { get; set; }

        /// <summary>
        /// Закреплённое сообщение в чате (возвращается только при запросе конкретного чата).
        /// </summary>
        public Message? PinnedMessage { get; set; }
    }
}
