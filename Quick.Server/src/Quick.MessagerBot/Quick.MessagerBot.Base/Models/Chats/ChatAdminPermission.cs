namespace Quick.MessagerBot.Base.Models.Chats
{
    /// <summary>
    /// Права администратора в чате.
    /// </summary>
    public enum ChatAdminPermission
    {
        /// <summary>
        /// Читать все сообщения.
        /// </summary>
        ReadAllMessages = 0,

        /// <summary>
        /// Добавлять/удалять участников.
        /// </summary>
        AddRemoveMembers = 1,

        /// <summary>
        /// Добавлять администраторов.
        /// </summary>
        AddAdmins = 2,

        /// <summary>
        /// Изменять информацию о чате.
        /// </summary>
        ChangeChatInfo = 3,

        /// <summary>
        /// Закреплять сообщения.
        /// </summary>
        PinMessage = 4,

        /// <summary>
        /// Писать сообщения.
        /// </summary>
        Write = 5,

        /// <summary>
        /// Изменять ссылку на чат.
        /// </summary>
        EditLink = 6
    }
}
