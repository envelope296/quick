using Quick.API.Extensions;
using Quick.DataAccess.Context.Extensions;

public class Program
{
    public static Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddCors();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddAuthorization();
        builder.Services.AddApplicationAuthentication(builder.Configuration);
        builder.Services.AddDbDataSource(builder.Configuration);
        builder.Services.AddApplicationDbContext();
        builder.Services.ConfigureOptions();
        builder.Services.AddRepositoryImplementations();
        builder.Services.AddServiceImplementations();
        builder.Services.AddUserContextAccessor();

        var app = builder.Build();

        app.UseRouting();
        app.UseCors(builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
        );
        app.UseHttpsRedirection();

        app.ConfigureMiddlewares();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        return app.RunAsync();
    }
}