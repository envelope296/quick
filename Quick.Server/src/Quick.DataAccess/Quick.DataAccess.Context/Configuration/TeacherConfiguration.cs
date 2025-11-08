using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quick.DataAccess.Models;

namespace Quick.DataAccess.Context.Configuration
{
    public class TeacherConfiguration : BaseEntityConfiguration<Teacher, Guid>
    {
        protected override void OnConfigure(EntityTypeBuilder<Teacher> builder)
        {
            builder.HasOne(t => t.Group)
                .WithMany(g => g.Teachers)
                .HasForeignKey(t => t.GroupId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
