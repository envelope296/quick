using Quick.MessagerBot.Base.Models.Users;

namespace Quick.MessagerBot.Base.Models.Updates
{
    /// <summary>
    /// Событие удаления бота из чата.
    /// </summary>
    public class BotRemovedUpdate : Update
    {
        /// <inheritdoc />
        public override UpdateType UpdateType => UpdateType.BotRemoved;

        /// <summary>
        /// ID чата, откуда был удален бот.
        /// </summary>
        public long ChatId { get; set; }

        /// <summary>
        /// Пользователь, удаливший бота из чата.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Указывает, был ли бот удален из канала или нет.
        /// </summary>
        public bool IsChannel { get; set; }

        public override BotRemovedUpdate AsBotRemoved => this;
    }
}
