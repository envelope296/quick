namespace Quick.MessagerBot.Base.Models.Users
{
    /// <summary>
    /// Объект пользователя с фотографией
    /// </summary>
    public class UserWithPhoto : User
    {
        /// <summary>
        /// Описание пользователя.
        /// Может быть null, если пользователь его не заполнил.
        /// Ограничения: до 16000 символов.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// URL аватара.
        /// </summary>
        public string? AvatarUrl { get; set; }

        /// <summary>
        /// URL аватара большего размера.
        /// </summary>
        public string? FullAvatarUrl { get; set; }
    }
}
