using Lock.Data;
using Lock.Data.Data;
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
        if (_mutexService.CurrentMutex.WaitOne(1000))
        {
            try
            {
                /*await using var context = await dbContextFactory.CreateDbContextAsync().ConfigureAwait(true);*/
                await DoImplementationAsync(Db).ConfigureAwait(true);
            }
            finally
            {
                _mutexService.CurrentMutex.ReleaseMutex();
            }
        }
    }

    public void ClearSync()
    {
        Db.Logs.ExecuteDelete();
        
        var counter = Db.Counters.First();
        
        counter.Value = 0;

        Db.SaveChanges();
    }

    public void Do()
    {
        using var context = dbContextFactory.CreateDbContext();

        _mutexService.CurrentMutex.WaitOne(1000);

        try
        {
            var counter = context.Counters.First();

            counter.Value++;

            var log = new Log
            {
                Message = $"Новое значение {counter.Value}",
                LoggedAt = DateTimeOffset.UtcNow
            };

            context.Logs.Add(log);

            context.SaveChanges();
        }
        finally
        {
            _mutexService.CurrentMutex.ReleaseMutex();
        }
    }
}