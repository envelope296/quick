using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Data.Common;
using System.Reflection;

namespace Quick.DataAccess.Context.Extensions
{
    public static class DbContextOptionsBuilderExtensions
    {
        public static void ConfigureDbMigrations(this DbContextOptionsBuilder builder, DbDataSource dataSource, Assembly assembly)
        {
            builder.UseNpgsql(dataSource, options =>
            {
                options.MigrationsHistoryTable(HistoryRepository.DefaultTableName);
                options.MigrationsAssembly(assembly.FullName);
            });
        }
    }
}
