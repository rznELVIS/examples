using Lock.Data;
using Lock.Logic;
using Microsoft.EntityFrameworkCore;

namespace MutexAccess;

public class ProcessService(
    LockDbContext db,
    IDbContextFactory<LockDbContext> dbContextFactory,
    MutexService mutexService) : BaseProcessService(db, dbContextFactory)
{
    private readonly MutexService _mutexService = mutexService;
    
    public override async Task DoAsync()
    {
        await Task.Run(() =>  _mutexService.CurrentMutex.WaitOne(1000));
        
        try
        {
            await using var context = await dbContextFactory.CreateDbContextAsync();
            await DoImplementationAsync(context).ConfigureAwait(true);
        }
        finally
        {
            _mutexService.CurrentMutex.ReleaseMutex();
        }
    }
}