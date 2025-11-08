using Quick.MessagerBot.Base.Models.Markup;

namespace Quick.MessagerBot.Base.Models.Messages
{
    /// <summary>
    /// Схема, представляющая тело сообщения.
    /// </summary>
    public class MessageBody
    {
        /// <summary>
        /// Уникальный ID сообщения.
        /// </summary>
        public string Mid { get; set; }

        /// <summary>
        /// ID последовательности сообщения в чате.
        /// </summary>
        public long Seq { get; set; }

        /// <summary>
        /// Новый текст сообщения.
        /// </summary>
        public string? Text { get; set; }

        //public Attachment[]? Attachments { get; set; }

        /// <summary>
        /// Разметка текста сообщения.
        /// </summary>
        public MarkupElement[]? Markup { get; set; }
    }
}
