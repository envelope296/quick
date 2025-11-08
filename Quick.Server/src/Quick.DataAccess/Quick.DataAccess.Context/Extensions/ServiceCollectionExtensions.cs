using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quick.DataAccess.Context.Contexts;
using Quick.DataAccess.Context.Factories;
using System.Data.Common;
using System.Reflection;

namespace Quick.DataAccess.Context.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDbDataSource(this IServiceCollection services, IConfiguration configuration)
        {
            var dataSourceFactory = new DbDataSourseFactory(configuration);
            var dataSource = dataSourceFactory.Build();
            services.AddSingleton<DbDataSource>(dataSource);
            return services;
        }

        public static IServiceCollection AddApplicationDbContext(this IServiceCollection services)
        {
            services.AddDbContext<DbContext, ApplicationDbContext>((serviceProvider, options) => {
                var dataSource = serviceProvider.GetRequiredService<DbDataSource>();
                options.UseNpgsql(dataSource);
            });
            return services;
        }

        public static IServiceCollection AddMigratorDbContext(this IServiceCollection services, Assembly assembly)
        {
            services.AddDbContext<DbContext, ApplicationDbContext>((serviceProvider, options) => {
                var dataSource = serviceProvider.GetRequiredService<DbDataSource>();
                options.ConfigureDbMigrations(dataSource, assembly);
            });
            return services;
        }
    }
}
