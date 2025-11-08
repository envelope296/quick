using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quick.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Quick.DataAccess.Context.Configuration
{
    public class ScheduleConfiguration : BaseEntityConfiguration<Schedule, Guid>
    {
        protected override void OnConfigure(EntityTypeBuilder<Schedule> builder)
        {
            builder.HasOne(s => s.Group)
                .WithMany(g => g.Schedules)
                .HasForeignKey(s => s.GroupId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
