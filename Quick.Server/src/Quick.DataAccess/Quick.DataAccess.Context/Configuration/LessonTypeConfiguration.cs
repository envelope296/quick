using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quick.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Quick.DataAccess.Context.Configuration
{
    public class LessonTypeConfiguration : BaseEntityConfiguration<LessonType, Guid>
    {
        protected override void OnConfigure(EntityTypeBuilder<LessonType> builder)
        {
            builder.HasOne(lt => lt.Schedule)
                .WithMany(s => s.LessonTypes)
                .HasForeignKey(lt => lt.ScheduleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
