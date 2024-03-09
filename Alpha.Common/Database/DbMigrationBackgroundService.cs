using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Alpha.Common.Database;

public class DbMigrationBackgroundService<T>(IServiceProvider serviceProvider) : BackgroundService
    where T : DbContext
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var scope = serviceProvider.CreateScope();
        var dataContext = scope.ServiceProvider.GetRequiredService<T>();

        dataContext.Database.Migrate();

        return Task.CompletedTask;
    }
}
