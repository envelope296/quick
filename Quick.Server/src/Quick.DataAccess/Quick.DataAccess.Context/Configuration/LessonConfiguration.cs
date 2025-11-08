using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quick.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Quick.DataAccess.Context.Configuration
{
    public class LessonConfiguration : BaseEntityConfiguration<Lesson, Guid>
    {
        protected override void OnConfigure(EntityTypeBuilder<Lesson> builder)
        {
            builder.HasOne(l => l.Subject)
                .WithMany(s => s.Lessons)
                .HasForeignKey(l => l.SubjectId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(l => l.TimeSlot)
                .WithMany(ts => ts.Lessons)
                .HasForeignKey(l => l.TimeSlotId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(l => l.Subgroup)
                .WithMany(s => s.Lessons)
                .HasForeignKey(l => l.SubgroupId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(l => l.Teacher)
                .WithMany(t => t.Lessons)
                .HasForeignKey(l => l.TeacherId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(l => l.LessonType)
                .WithMany(lt => lt.Lessons)
                .HasForeignKey(l => l.LessonTypeId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
