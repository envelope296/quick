using Quick.MessagerBot.Base.Models.Messages;

namespace Quick.MessagerBot.Base.Models.Updates
{
    /// <summary>
    /// Событие нажатия на кнопку.
    /// </summary>
    public class MessageCallbackUpdate : Update
    {
        /// <inheritdoc />
        public override UpdateType UpdateType => UpdateType.MessageCallback;

        /// <summary>
        /// Объект, отправленный боту, когда пользователь нажимает кнопку.
        /// </summary>
        public Callback Callback { get; set; }

        /// <summary>
        /// Изначальное сообщение, содержащее встроенную клавиатуру. 
        /// Может быть null, если оно было удалено к моменту, когда бот получил это обновление.
        /// </summary>
        public Message? Message { get; set; }

        /// <summary>
        /// Текущий язык пользователя в формате IETF BCP 47. 
        /// Доступно только в диалогах.
        /// </summary>
        public string? UserLocale { get; set; }

        public override MessageCallbackUpdate AsMessageCallback => this;
    }
}
