using Getapplock.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace Getapplock.Console;

public class ManageService
{
    private readonly IDbContextFactory<GetApplockDbContext> _contextFactory;

    public ManageService(IDbContextFactory<GetApplockDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task DoAsync(CancellationToken cancellationToken = default)
    {
        var tasks = new List<Task>();
        
        for (int i = 0; i < 50; i++)
        {
            var task = UpdateCounterAsync(i, cancellationToken);
            tasks.Add(task);
        }
        
        await Task.WhenAll(tasks);
        
   }

    private async Task UpdateCounterAsync(int iteration, CancellationToken cancellationToken = default)
    {
        System.Console.WriteLine($"UpdateCounterAsync выполняется для итерации: {iteration}");
        
        await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        var counters = await context.Counters.ToListAsync(cancellationToken);
        
        System.Console.WriteLine($"Итерация {iteration}: получено {counters.Count} записей");
        // Здесь можно добавить логику обработки counters
    }
}
