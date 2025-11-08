namespace Quick.MessagerBot.Base.Models.Markup
{
    /// <summary>
    /// Тип разметки.
    /// </summary>
    public enum MarkupType
    {
        /// <summary>
        /// Жирный.
        /// </summary>
        Strong = 0,
        
        /// <summary>
        /// Курсив.
        /// </summary>
        Emphasized = 1,

        /// <summary>
        /// Моноширный.
        /// </summary>
        Monospased = 2,

        /// <summary>
        /// Ссылка.
        /// </summary>
        Link = 3,
        
        /// <summary>
        /// Зачеркнутый.
        /// </summary>
        Strikethrough = 4,

        /// <summary>
        /// Подчеркнутый.
        /// </summary>
        Underline = 5,

        /// <summary>
        /// Упоминание пользователя.
        /// </summary>
        UserMention = 6,
    }
}
