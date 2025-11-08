namespace Quick.MessagerBot.Base.Models.Responses
{
    public abstract class BaseMarkerResponse
    {
        /// <summary>
        /// Указатель на следующую страницу данных.
        /// </summary>
        public long? Marker { get; set; }
    }
}
