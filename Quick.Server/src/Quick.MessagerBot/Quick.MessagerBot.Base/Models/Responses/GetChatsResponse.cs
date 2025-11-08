using Quick.MessagerBot.Base.Models.Chats;

namespace Quick.MessagerBot.Base.Models.Responses
{
    public class GetChatsResponse : BaseMarkerResponse
    {
        /// <summary>
        /// Список запрашиваемых чатов.
        /// </summary>
        public Chat[] Chats { get; set; }
    }
}
