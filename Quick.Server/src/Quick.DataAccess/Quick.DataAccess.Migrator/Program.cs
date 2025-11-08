using Quick.DataAccess.Context.Extensions;
using Quick.DataAccess.Migrator.Workers;
using System.Reflection;

public class Program
{
    public static async Task Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices(ServicesConfiguration)
            .Build();
        await host.RunAsync();
    }

    private static void ServicesConfiguration(HostBuilderContext context, IServiceCollection services)
    {
        services.AddDbDataSource(context.Configuration);
        services.AddMigratorDbContext(Assembly.GetExecutingAssembly());
        services.AddHostedService<MigrationWorker>();
    }
}