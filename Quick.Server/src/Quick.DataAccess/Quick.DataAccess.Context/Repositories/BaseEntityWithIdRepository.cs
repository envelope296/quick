using Microsoft.EntityFrameworkCore;
using Quick.Common.DataAccess.Abstraction.Models;
using Quick.Common.DataAccess.Abstraction.Repositories;
using System.Linq.Expressions;

namespace Quick.DataAccess.Context.Repositories
{
    public abstract class BaseEntityWithIdRepository<TDbContext, TEntity, TEntityId> : BaseEntityRepository<TDbContext, TEntity>, IEntityWithIdRepository<TEntity, TEntityId>
        where TEntityId : notnull, IEquatable<TEntityId>
        where TEntity : BaseEntity<TEntityId>
        where TDbContext : DbContext
    {
        public BaseEntityWithIdRepository(TDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<TEntity?> GetAsync(TEntityId id, CancellationToken cancellationToken)
        {
            var result = await GetAsync(PredicateById(id), cancellationToken);
            return result;
        }

        public async Task<bool> CheckIfExistsAsync(TEntityId id, CancellationToken cancellationToken)
        {
            var result = await CheckIfExistsAsync(PredicateById(id), cancellationToken);
            return result;
        }

        public async Task<int> ExecuteDeleteAsync(TEntityId id, CancellationToken cancellationToken)
        {
            var result = await ExecuteDeleteAsync(PredicateById(id), cancellationToken);
            return result;
        }

        public IQueryable<TEntity> Filter(TEntityId id)
        {
            return Filter(PredicateById(id));
        }

        protected Expression<Func<TEntity, bool>> PredicateById(TEntityId id) => entity => entity.Id.Equals(id);
    }
}
