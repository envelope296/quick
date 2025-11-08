using Quick.MessagerBot.Base.Models.Messages;

namespace Quick.MessagerBot.Base.Models.Updates
{
    /// <summary>
    /// Событие создания сообщения.
    /// </summary>
    public class MessageCreatedUpdate : Update
    {
        /// <inheritdoc />
        public override UpdateType UpdateType => UpdateType.MessageCreated;

        /// <summary>
        /// Новое созданное сообщение
        /// </summary>
        public Message Message { get; set; }

        /// <summary>
        /// Текущий язык пользователя в формате IETF BCP 47. 
        /// Доступно только в диалогах.
        /// </summary>
        public string? UserLocale { get; set; }

        public override MessageCreatedUpdate AsMessageCreated => this;
    }
}
