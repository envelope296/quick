using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quick.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Quick.DataAccess.Context.Configuration
{
    public class SubjectConfiguration : BaseEntityConfiguration<Subject, Guid>
    {
        protected override void OnConfigure(EntityTypeBuilder<Subject> builder)
        {
            builder.HasOne(s => s.Group)
                .WithMany(g => g.Subjects)
                .HasForeignKey(s => s.GroupId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
