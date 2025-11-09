using Quick.Common.DataAccess.Abstraction.Models;
using System.Linq.Expressions;

namespace Quick.Common.DataAccess.Abstraction.Repositories
{
    public interface IRepository<TEntity, TEntityId>
        where TEntityId : notnull, IEquatable<TEntityId>
        where TEntity : BaseEntity<TEntityId>
    {
        Task<TEntityId?> GetIdAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

        Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

        Task<TEntity?> GetAsync(TEntityId id, CancellationToken cancellationToken);

        Task<bool> CheckIfExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

        Task<bool> CheckIfExistsAsync(TEntityId id, CancellationToken cancellationToken);

        Task<int> ExecuteDeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

        Task<int> ExecuteDeleteAsync(TEntityId id, CancellationToken cancellationToken);

        void Add(TEntity entity);

        void AddRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);

        void RemoveRange(IEnumerable<TEntity> entities);

        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
