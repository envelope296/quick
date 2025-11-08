namespace Quick.MessagerBot.Base.Models.Messages
{
    /// <summary>
    /// Тип связанного сообщения.
    /// </summary>
    public enum MessageLinkType
    {
        /// <summary>
        /// Пересланное сообщение.
        /// </summary>
        Forward = 0,

        /// <summary>
        /// Ответное сообщение.
        /// </summary>
        Reply = 1,
    }
}
