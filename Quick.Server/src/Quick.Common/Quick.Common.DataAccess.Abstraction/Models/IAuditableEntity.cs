namespace Quick.Common.DataAccess.Abstraction.Models
{
    /// <summary>
    /// Сущность базы данных с аудированием даты и времени создания/обновления.
    /// </summary>
    public interface IAuditableEntity
    {
        /// <summary>
        /// Дата и время создания.
        /// </summary>
        public DateTimeOffset CreatedOn { get; set; }

        /// <summary>
        /// Дата и время обновления.
        /// </summary>
        public DateTimeOffset? UpdatedOn { get; set; }
    }
}
