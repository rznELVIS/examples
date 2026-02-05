using Microsoft.Extensions.DependencyInjection;

namespace PgAdvisoryLock.Services;

public class ManagerService
{
    public async Task Do(ServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            for (int i = 0; i < 100; i++)
            {
                var process = scope.ServiceProvider.GetRequiredService<ProcessService>();
                await process.DoAsync(new Random());
            }
        }
    }
}