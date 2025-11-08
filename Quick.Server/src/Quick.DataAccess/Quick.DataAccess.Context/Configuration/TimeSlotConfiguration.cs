using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quick.DataAccess.Models;

namespace Quick.DataAccess.Context.Configuration
{
    public class TimeSlotConfiguration : BaseEntityConfiguration<TimeSlot, Guid>
    {
        protected override void OnConfigure(EntityTypeBuilder<TimeSlot> builder)
        {
            builder.HasOne(ts => ts.Schedule)
                .WithMany(s => s.TimeSlots)
                .HasForeignKey(ts => ts.ScheduleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
