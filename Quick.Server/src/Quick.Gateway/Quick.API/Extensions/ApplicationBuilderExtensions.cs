using Quick.API.Middlewares;

namespace Quick.API.Extensions
{
    internal static class ApplicationBuilderExtensions
    {
        internal static IApplicationBuilder ConfigureMiddlewares(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();
            return app;
        }
    }
}
