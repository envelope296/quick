using Microsoft.EntityFrameworkCore;

namespace Quick.DataAccess.Migrator.Workers
{
    internal class MigrationWorker : BackgroundService
    {
        private readonly ILogger<MigrationWorker> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly IHostApplicationLifetime _lifetime;

        public MigrationWorker(
            ILogger<MigrationWorker> logger, 
            IServiceProvider serviceProvider, 
            IHostApplicationLifetime lifetime)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _lifetime = lifetime;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                using (var context = scope.ServiceProvider.GetRequiredService<DbContext>())
                {
                    _logger.LogInformation("Начало миграции БД");
                    context.Database.Migrate();
                    _logger.LogInformation("Миграция БД успешно завершена");
                }

                _lifetime.StopApplication();
                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Неожиданная ошибка при миграции БД");
                Environment.ExitCode = 4313;
                throw;
            }
        }
    }
}
