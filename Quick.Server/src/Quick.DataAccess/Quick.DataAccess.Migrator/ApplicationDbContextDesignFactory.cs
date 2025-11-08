using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Quick.DataAccess.Context.Contexts;
using Quick.DataAccess.Context.Extensions;
using Quick.DataAccess.Context.Factories;
using System.Reflection;

namespace Quick.DataAccess.Migrator
{
    public class ApplicationDbContextDesignFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .Build();

            var dataSourseFactory = new DbDataSourseFactory(configuration);
            var dataSourse = dataSourseFactory.Build();

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.ConfigureDbMigrations(dataSourse, Assembly.GetExecutingAssembly());

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
