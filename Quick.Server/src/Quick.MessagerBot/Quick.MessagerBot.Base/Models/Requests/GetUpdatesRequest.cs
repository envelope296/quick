using Quick.MessagerBot.Base.Models.Updates;

namespace Quick.MessagerBot.Base.Models.Requests
{
    public class GetUpdatesRequest : BaseMarkerRequest
    {
        /// <summary>
        /// Максимальное количество обновлений для получения.
        /// По умолчанию: 100.
        /// </summary>
        public int? Limit { get; set; }

        /// <summary>
        /// Тайм-аут в секундах для долгого опроса.
        /// По умолчанию: 30.
        /// </summary>
        public int? Timeout { get; set; }

        /// <summary>
        /// Список типов обновлений.
        /// </summary>
        public UpdateType[]? Types { get; set; }
    }
}
