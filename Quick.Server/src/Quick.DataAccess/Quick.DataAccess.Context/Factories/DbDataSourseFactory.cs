using Microsoft.Extensions.Configuration;
using Npgsql;
using Quick.DataAccess.Context.Contexts;
using Quick.DataAccess.Models;
using System.Data.Common;
using DayOfWeek = Quick.DataAccess.Models.DayOfWeek;

namespace Quick.DataAccess.Context.Factories
{
    public class DbDataSourseFactory
    {
        private readonly IConfiguration _configuration;

        public DbDataSourseFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbDataSource Build()
        {
            var connectionString = _configuration.GetConnectionString(nameof(ApplicationDbContext));
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);

            dataSourceBuilder.MapEnum<DayOfWeek>();
            dataSourceBuilder.MapEnum<WeekType>();

            return dataSourceBuilder.Build();
        }
    }
}
