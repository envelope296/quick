using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quick.DataAccess.Models;

namespace Quick.DataAccess.Context.Configuration
{
    public class GroupMemberConfiguration : IEntityTypeConfiguration<GroupMember>
    {
        public void Configure(EntityTypeBuilder<GroupMember> builder)
        {
            builder.HasKey(gm => new { gm.GroupId, gm.UserId });

            builder.HasOne(gm => gm.Group)
                .WithMany(g => g.Members)
                .HasForeignKey(gm => gm.GroupId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(gm => gm.User)
                .WithMany(u => u.GroupMembers)
                .HasForeignKey(gm => gm.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(gm => gm.Subgroup)
                .WithMany(s => s.GroupMembers)
                .HasForeignKey(gm => gm.SubgroupId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
