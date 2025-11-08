namespace Quick.MessagerBot.Base.Models.Chats
{
    /// <summary>
    /// Статус чата.
    /// </summary>
    public enum ChatStatus
    {
        /// <summary>
        /// Бот является активным участником чата.
        /// </summary>
        Active = 0,

        /// <summary>
        /// Бот был удалён из чата.
        /// </summary>
        Removed = 1,

        /// <summary>
        /// Бот покинул чат.
        /// </summary>
        Left = 2,

        /// <summary>
        /// Чат был закрыт.
        /// </summary>
        Closed = 3,
    }
}
