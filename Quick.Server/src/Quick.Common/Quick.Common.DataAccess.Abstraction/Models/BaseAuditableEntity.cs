namespace Quick.Common.DataAccess.Abstraction.Models
{
    /// <summary>
    /// Базовая реализация сущности базы данных с аудированием даты и времени добавления/обновления.
    /// </summary>
    /// <typeparam name="TEntityId">Тип идентификатора сущности.</typeparam>
    public class BaseAuditableEntity<TEntityId> : BaseEntity<TEntityId>, IAuditableEntity
        where TEntityId : notnull, IEquatable<TEntityId>
    {
        /// <inheritdoc/>
        public DateTimeOffset CreatedOn { get; set; }

        /// <inheritdoc/>
        public DateTimeOffset? UpdatedOn { get; set; }
    }
}
