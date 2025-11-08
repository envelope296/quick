namespace Quick.MessagerBot.Base.Models.Requests
{
    public class GetChatsRequest : BaseMarkerRequest
    {
        /// <summary>
        /// Количество запрашиваемых чатов.
        /// По умолчанию: 50.
        /// </summary>
        public int? Chat { get; set; }
    }
}
