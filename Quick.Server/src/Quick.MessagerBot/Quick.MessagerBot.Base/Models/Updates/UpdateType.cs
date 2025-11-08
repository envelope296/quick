namespace Quick.MessagerBot.Base.Models.Updates
{
    /// <summary>
    /// Тип события.
    /// </summary>
    public enum UpdateType
    {
        /// <summary>
        /// Неизвестно.
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// Создание сообщения.
        /// </summary>
        MessageCreated = 1,

        /// <summary>
        /// Нажатие на кнопку.
        /// </summary>
        MessageCallback = 2,

        /// <summary>
        /// Бот добавлен в чат.
        /// </summary>
        BotAdded = 3,

        /// <summary>
        /// Бот удален из чата.
        /// </summary>
        BotRemoved = 4,
    }
}
