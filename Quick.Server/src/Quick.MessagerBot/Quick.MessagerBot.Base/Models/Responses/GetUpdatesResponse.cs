using Quick.MessagerBot.Base.Models.Updates;

namespace Quick.MessagerBot.Base.Models.Responses
{
    public class GetUpdatesResponse : BaseMarkerResponse
    {
        /// <summary>
        /// Страница обновлений.
        /// </summary>
        public Update[] Updates { get; set; }
    }
}
