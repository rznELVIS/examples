using Lock.Data;
using Lock.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace Lock.Logic;

public abstract class BaseProcessService(
    LockDbContext db,
    IDbContextFactory<LockDbContext> dbContextFactory)
{
    protected readonly LockDbContext Db = db;
    protected readonly IDbContextFactory<LockDbContext> DbFactory = dbContextFactory;

    public abstract Task DoAsync();
    
    public async Task Clear()
    {
        await Db.Logs.ExecuteDeleteAsync();
        
        var counter = Db.Counters.First();
        
        counter.Value = 0;
        
        await Db.SaveChangesAsync();
    }
    
    protected virtual async Task DoImplementationAsync(LockDbContext context)
    {
        var counter = await context.Counters.FirstAsync();

        counter.Value++;

        var log = new Log
        {
            Message = $"Новое значение {counter.Value}",
            LoggedAt = DateTimeOffset.UtcNow
        };
        
        context.Logs.Add(log);
        
        await context.SaveChangesAsync();
    }
}