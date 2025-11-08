using Quick.MessagerBot.Base.Models.Users;

namespace Quick.MessagerBot.Base.Models.Updates
{
    /// <summary>
    /// Событие добавления бота в чат.
    /// </summary>
    public class BotAddedUpdate : Update
    {
        /// <inheritdoc />
        public override UpdateType UpdateType => UpdateType.BotAdded;

        /// <summary>
        /// ID чата, куда был добавлен бот.
        /// </summary>
        public long ChatId { get; set; }

        /// <summary>
        /// Пользователь, добавивший бота в чат.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Указывает, был ли бот добавлен в канал или нет.
        /// </summary>
        public bool IsChannel { get; set; }

        public override BotAddedUpdate AsBotAdded => this;
    }
}
