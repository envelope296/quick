using Quick.MessagerBot.Base.Models.Users;

namespace Quick.MessagerBot.Base.Models.Bot
{
    /// <summary>
    /// Объект, описывающий информацию о боте
    /// </summary>
    public class BotInfo : UserWithPhoto
    {
        /// <summary>
        /// Команды, поддерживаемые ботом.
        /// Ограничения: до 32 элементов.
        /// </summary>
        public BotCommand[]? Commands { get; set; }

        /// <inheritdoc />
        public override bool IsBot => true;
    }
}
