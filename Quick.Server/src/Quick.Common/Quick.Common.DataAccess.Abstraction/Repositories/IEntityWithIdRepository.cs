using Quick.Common.DataAccess.Abstraction.Models;

namespace Quick.Common.DataAccess.Abstraction.Repositories
{
    public interface IEntityWithIdRepository<TEntity, TEntityId> : IEntityRepository<TEntity>
        where TEntityId : notnull, IEquatable<TEntityId>
        where TEntity : BaseEntity<TEntityId>
    {
        IQueryable<TEntity> Filter(TEntityId id);

        Task<TEntity?> GetAsync(TEntityId id, CancellationToken cancellationToken);

        Task<bool> CheckIfExistsAsync(TEntityId id, CancellationToken cancellationToken);

        Task<int> ExecuteDeleteAsync(TEntityId id, CancellationToken cancellationToken);
    }
}
