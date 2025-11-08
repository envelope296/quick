namespace Quick.MessagerBot.Base.Models.Users
{
    /// <summary>
    /// Базовый объект, описывающий пользователя.
    /// </summary>
    public abstract class UserBase
    {
        /// <summary>
        /// ID пользователя.
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// Отображаемое имя пользователя.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Отображаемая фамилия пользователя.
        /// </summary>
        public string? LastName { get; set; }

        /// <summary>
        /// Уникальное публичное имя пользователя. 
        /// Может быть null, если пользователь недоступен или имя не задано
        /// </summary>
        public string? Username { get; set; }

        /// <summary>
        /// Время последней активности пользователя в MAX(Unix-время в миллисекундах). 
        /// Может быть неактуальным, если пользователь отключил статус "онлайн" в настройках.
        /// </summary>
        public long LastActivityTime { get; set; }

        /// <summary>
        /// true, если пользователь является ботом.
        /// </summary>
        public abstract bool IsBot { get; set; }
    }
}
