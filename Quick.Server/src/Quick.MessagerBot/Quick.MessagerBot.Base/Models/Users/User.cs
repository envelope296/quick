namespace Quick.MessagerBot.Base.Models.Users
{
    /// <summary>
    /// Объект, описывающий пользователя.
    /// </summary>
    public class User : UserBase
    {
        /// <inheritdoc />
        public override bool IsBot { get; set; }
    }
}
