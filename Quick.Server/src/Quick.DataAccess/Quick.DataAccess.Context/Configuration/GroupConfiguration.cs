using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Quick.DataAccess.Models;

namespace Quick.DataAccess.Context.Configuration
{
    public class GroupConfiguration : BaseEntityConfiguration<Group, Guid>
    {
        protected override void OnConfigure(EntityTypeBuilder<Group> builder)
        {
            builder.HasOne(g => g.Owner)
                .WithMany(u => u.Groups)
                .HasForeignKey(g => g.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
