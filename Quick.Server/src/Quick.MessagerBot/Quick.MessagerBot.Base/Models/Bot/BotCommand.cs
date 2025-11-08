namespace Quick.MessagerBot.Base.Models.Bot
{
    /// <summary>
    /// Комманда, поддерживаемая ботом
    /// </summary>
    public class BotCommand
    {
        /// <summary>
        /// Название команды.
        /// Ограничения: от 1 до 64 символов.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание команды (по желанию).
        /// Ограничения: от 1 до 128 символов.
        /// </summary>
        public string? Description { get; set; }
    }
}
