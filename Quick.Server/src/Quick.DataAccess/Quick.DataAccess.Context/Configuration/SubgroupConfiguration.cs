using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quick.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Quick.DataAccess.Context.Configuration
{
    public class SubgroupConfiguration : BaseEntityConfiguration<Subgroup, Guid>
    {
        protected override void OnConfigure(EntityTypeBuilder<Subgroup> builder)
        {
            builder.HasOne(s => s.Group)
                .WithMany(g => g.Subgroups)
                .HasForeignKey(s => s.GroupId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
