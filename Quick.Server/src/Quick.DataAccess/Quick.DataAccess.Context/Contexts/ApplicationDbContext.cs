using Microsoft.EntityFrameworkCore;
using Quick.DataAccess.Models;
using DayOfWeek = Quick.DataAccess.Models.DayOfWeek;

namespace Quick.DataAccess.Context.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IAssemblyMarker).Assembly);
            
            modelBuilder.HasPostgresEnum<DayOfWeek>();
            modelBuilder.HasPostgresEnum<WeekType>();
            modelBuilder.HasPostgresEnum<ScheduleType>();
        }

        public DbSet<Group> Groups { get; set; }

        public DbSet<GroupMember> GroupMembers { get; set; }

        public DbSet<Lesson> Lessons { get; set; }

        public DbSet<LessonType> LessonTypes { get; set; }

        public DbSet<Schedule> Schedules { get; set; }

        public DbSet<Subgroup> Subgroups { get; set; }

        public DbSet<Subject> Subjects { get; set; }
        
        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<TimeSlot> TimeSlots { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
