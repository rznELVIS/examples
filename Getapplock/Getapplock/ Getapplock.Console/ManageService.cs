using Getapplock.Data;
using Microsoft.EntityFrameworkCore;

namespace Getapplock.Console;

public class ManageService
{
    private static readonly Random Random = new();
    private const int MaxDelayMs = 100;
    private const int IterationCount = 1;
    
    private readonly IDbContextFactory<GetApplockDbContext> _contextFactory;

    public ManageService(IDbContextFactory<GetApplockDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task DoAsync(CancellationToken cancellationToken = default)
    {
        System.Console.WriteLine("Обновление начато.");
        
        await ClearDataAsync(cancellationToken);
        
        var tasks = new List<Task>();
        
        for (var i = 0; i < IterationCount; i++)
        {
            var task = UpdateCounterAsync(i, cancellationToken);
            tasks.Add(task);
        }
        
        await Task.WhenAll(tasks);
    }

    private async Task ClearDataAsync(CancellationToken cancellationToken = default)
    {
        await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        
        await context.Logs.ExecuteDeleteAsync(cancellationToken);
        
        var counter = await context
            .Counters
            .FirstAsync(cancellationToken);
        counter.Value = 0;
        
        await context.SaveChangesAsync(cancellationToken);
    }

    private async Task UpdateCounterAsync(int iteration, CancellationToken cancellationToken = default)
    {
        await RandomDelayAsync(cancellationToken);

        await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        
        var counter = await context.Counters.FirstAsync(cancellationToken);

        counter.Value++;
        
        await context.SaveChangesAsync(cancellationToken);
        System.Console.WriteLine($"Итерация {iteration} закончена.");
    }
    
    private async Task RandomDelayAsync(CancellationToken cancellationToken = default)
    {
        var delayMs = Random.Next(0, MaxDelayMs + 1);
        await Task.Delay(delayMs, cancellationToken);
    }
}
