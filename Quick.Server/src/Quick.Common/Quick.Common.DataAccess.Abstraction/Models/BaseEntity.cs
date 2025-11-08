namespace Quick.Common.DataAccess.Abstraction.Models
{
    /// <summary>
    /// Базовая сущность базы данных.
    /// </summary>
    /// <typeparam name="TEntityId">Тип идентификатора сущности</typeparam>
    public class BaseEntity<TEntityId> where TEntityId : notnull, IEquatable<TEntityId>
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public TEntityId Id { get; set; }
    }
}
