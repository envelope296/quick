using Microsoft.EntityFrameworkCore;
using Quick.Common.DataAccess.Abstraction.Models;
using Quick.Common.DataAccess.Abstraction.Repositories;
using System.Linq.Expressions;

namespace Quick.DataAccess.Context.Repositories
{
    public abstract class BaseRepository<TDbContext, TEntity, TEntityId> : IRepository<TEntity, TEntityId>
        where TEntityId : notnull, IEquatable<TEntityId>
        where TEntity : BaseEntity<TEntityId>
        where TDbContext : DbContext
    {
        private static EntityState[] AuditableEntityStates = [EntityState.Modified, EntityState.Added];

        private readonly TDbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public BaseRepository(TDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }

        protected DbSet<TEntity> DbSet => _dbSet;

        public Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            return _dbSet.FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public async Task<TEntity?> GetAsync(TEntityId id, CancellationToken cancellationToken)
        {
            var result = await GetAsync(PredicateById(id), cancellationToken);
            return result;
        }

        public Task<bool> CheckIfExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            return _dbSet.AnyAsync(predicate, cancellationToken);
        }

        public async Task<bool> CheckIfExistsAsync(TEntityId id, CancellationToken cancellationToken)
        {
            var result = await _dbSet.AnyAsync(PredicateById(id), cancellationToken);
            return result;
        }

        public Task<int> ExecuteDeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            return Filter(predicate).ExecuteDeleteAsync(cancellationToken);
        }

        public async Task<int> ExecuteDeleteAsync(TEntityId id, CancellationToken cancellationToken)
        {
            var result = await ExecuteDeleteAsync(PredicateById(id), cancellationToken);
            return result;
        }

        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            UpdateAuditableEntities();
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public IQueryable<TEntity> All => DbSet;

        public IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public IQueryable<TEntity> Filter(TEntityId id)
        {
            return Filter(PredicateById(id));
        }

        protected Expression<Func<TEntity, bool>> PredicateById(TEntityId id) => entity => entity.Id.Equals(id);

        private void UpdateAuditableEntities()
        {
            var currentDatetime = DateTimeOffset.UtcNow;
            var auditableEntities = _dbContext.ChangeTracker
                .Entries<IAuditableEntity>()
                .Where(entry => AuditableEntityStates.Contains(entry.State));
            
            foreach (var entry in auditableEntities)
            {
                var state = entry.State;
                var entity = entry.Entity;

                switch (state)
                {
                    case EntityState.Added:
                        entity.CreatedOn = currentDatetime;
                        break;
                    case EntityState.Modified:
                        entity.UpdatedOn = currentDatetime;
                        break;
                }
            }
        }
    }
}
