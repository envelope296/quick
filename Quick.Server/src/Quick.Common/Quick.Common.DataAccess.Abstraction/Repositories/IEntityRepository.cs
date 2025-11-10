using System.Linq.Expressions;

namespace Quick.Common.DataAccess.Abstraction.Repositories
{
    public interface IEntityRepository<TEntity>
        where TEntity : class
    {
        IQueryable<TEntity> All { get; }

        IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

        Task<bool> CheckIfExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

        Task ExecuteAddAsync(TEntity entity, CancellationToken cancellationToken);

        Task<int> ExecuteDeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

        void Add(TEntity entity);

        void AddRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);

        void RemoveRange(IEnumerable<TEntity> entities);

        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
