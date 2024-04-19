using Dapr.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Alpha.Tools.Database;

public class DbMigrationBackgroundService<T>(IServiceProvider serviceProvider, 
    ILogger<DbMigrationBackgroundService<T>> logger) : BackgroundService
    where T : DbContext
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var scope = serviceProvider.CreateScope();
        var dataContext = scope.ServiceProvider.GetRequiredService<T>();

        using var daprClient = new DaprClientBuilder().Build();

        // Using distributed lock
        var randomId = Guid.NewGuid().ToString();
        var resource = daprClient.GetType().FullName;
        logger.LogInformation($"Migration >>> Locking resource '{resource}'");
        await using var fileLock = await daprClient.Lock("lockstore", resource, randomId, 60, stoppingToken);

        if (fileLock.Success)
        {
            logger.LogInformation("Migration >>> Starting");
            dataContext.Database.Migrate();
            logger.LogInformation("Migration >>> Finished");
        }
        else
        {
            logger.LogError("Migration >>> Failed to LOCK dapr resource for migration");
        }
    }
}
