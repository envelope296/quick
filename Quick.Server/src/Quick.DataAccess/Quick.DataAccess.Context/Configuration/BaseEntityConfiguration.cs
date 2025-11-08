using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quick.Common.DataAccess.Abstraction.Models;

namespace Quick.DataAccess.Context.Configuration
{
    public abstract class BaseEntityConfiguration<TEntity, TEntityId> : IEntityTypeConfiguration<TEntity>
        where TEntityId : notnull, IEquatable<TEntityId>
        where TEntity : BaseEntity<TEntityId>
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(entity => entity.Id);
            OnConfigure(builder);
        }

        protected abstract void OnConfigure(EntityTypeBuilder<TEntity> builder);
    }
}
