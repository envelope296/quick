using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quick.DataAccess.Models;

namespace Quick.DataAccess.Context.Configuration
{
    public class UserConfiguration : BaseEntityConfiguration<User, long>
    {
        protected override void OnConfigure(EntityTypeBuilder<User> builder)
        {
            builder.HasOne(u => u.CurrentSchedule)
                .WithMany()
                .HasForeignKey(u => u.CurrentScheduleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
