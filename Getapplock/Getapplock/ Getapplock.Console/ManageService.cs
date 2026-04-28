using System.Data;
using Getapplock.Data;
using Getapplock.Data.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Getapplock.Console;

public class ManageService
{
    private static readonly Random Random = new();
    private const int MaxDelayMs = 100;
    private const int IterationCount = 100;
    
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
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var lockResult = await ExecuteSpGetAppLockAsync(
                context,
                lockTimeout: 50,
                cancellationToken: cancellationToken);

            if (lockResult >= 0)
            {
                System.Console.WriteLine($"Блокировка захвачена для итерации: {iteration}. Значение sp_getapplock: {lockResult}");
                
                var counter = await context.Counters.FirstAsync(cancellationToken);
                counter.Value++;

                var log = new Log
                {
                    Message = $"Iteration: {iteration}",
                    LoggedAt = DateTimeOffset.Now
                };
                await context.Logs.AddAsync(log, cancellationToken);
                
                await context.SaveChangesAsync(cancellationToken);

                await RandomDelayAsync(cancellationToken);
                
                await transaction.CommitAsync(cancellationToken);
                System.Console.WriteLine($"Итерация {iteration} закончена.");
            }
            else
            {
                System.Console.WriteLine($"Блокировка не захвачена для итерации: {iteration}");
            }
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
    
    private async Task<int> ExecuteSpGetAppLockAsync(
        GetApplockDbContext context,
        string resource = "testLock",
        string lockMode = "Exclusive",
        string lockOwner = "Transaction",
        int lockTimeout = 0,
        CancellationToken cancellationToken = default)
    {
        var returnValue = new SqlParameter
        {
            ParameterName = "@ReturnValue",
            SqlDbType = SqlDbType.Int,
            Direction = ParameterDirection.Output
        };
        
        await context.Database.ExecuteSqlRawAsync(
            "EXEC @ReturnValue = sp_getapplock @Resource, @LockMode, @LockOwner, @LockTimeout",
            [
                returnValue, 
                new SqlParameter("@Resource", resource), 
                new SqlParameter("@LockMode", lockMode),
                new SqlParameter("@LockOwner", lockOwner),
                new SqlParameter("@LockTimeout", lockTimeout),
            ],
            cancellationToken);
        
        return (int)returnValue.Value!;
    }

    private async Task RandomDelayAsync(CancellationToken cancellationToken = default)
    {
        var delayMs = Random.Next(0, MaxDelayMs + 1);
        await Task.Delay(delayMs, cancellationToken);
    }
}
