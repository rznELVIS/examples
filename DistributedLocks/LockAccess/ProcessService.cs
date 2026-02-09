using Lock.Data;
using Lock.Logic;
using Microsoft.EntityFrameworkCore;

namespace LockAccess;

public class ProcessService(LockDbContext db, IDbContextFactory<LockDbContext> dbContextFactory) : BaseProcessService(db, dbContextFactory)
{
    private readonly SemaphoreSlim _semaphore = new(1, 1);
    
    public override async Task DoAsync()
    {
        await _semaphore.WaitAsync();

        try
        {
            await using var context = await dbContextFactory.CreateDbContextAsync();

            await DoImplementationAsync(context);
        }
        finally
        {
            _semaphore.Release(1);
        }
    }
}