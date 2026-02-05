using Microsoft.Extensions.DependencyInjection;

namespace PgAdvisoryLock.Services;

public class ConcurrentManagerService
{
    private readonly Random _random;

    public ConcurrentManagerService()
    {
        _random = new Random();
    }
    
    public async Task Do(ServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var tasks = new List<Task>();
            
            for (int i = 0; i < 100; i++)
            {
                var process = scope.ServiceProvider.GetRequiredService<ProcessService>();
                tasks.Add(process.DoAsync(_random));
            }

            await Task.WhenAll(tasks);
        }
    }
}