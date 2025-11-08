namespace Quick.MessagerBot.Base.Models.Markup
{
    /// <summary>
    /// Элемент разметки.
    /// </summary>
    public class MarkupElement
    {
        /// <summary>
        /// Тип элемента разметки.
        /// </summary>
        public MarkupType Type { get; set; }

        /// <summary>
        /// Индекс начала элемента разметки в тексте. 
        /// Нумерация с нуля.
        /// </summary>
        public int From { get; set; }

        /// <summary>
        /// Длина элемента разметки.
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// URL ссылки.
        /// </summary>
        public string? Url { get; set; }

        /// <summary>
        /// @username упомянутого пользователя.
        /// </summary>
        public string? UserLink { get; set; }

        /// <summary>
        /// ID упомянутого пользователя без username.
        /// </summary>
        public long? UserId { get; set; }
    }
}
