namespace Quick.MessagerBot.Base.Models.Requests
{
    public abstract class BaseMarkerRequest
    {
        /// <summary>
        /// Указатель на страницу данных.
        /// </summary>
        public long? Marker { get; set; }
    }
}
