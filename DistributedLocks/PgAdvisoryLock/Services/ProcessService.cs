using Lock.Data;
using Lock.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace PgAdvisoryLock.Services;

public class ProcessService
{
    private readonly LockDbContext _db;

    public ProcessService(
        LockDbContext db)
    {
        _db = db;
    }
    
    public async Task DoAsync(Random random)
    {
        Thread.Sleep(random.Next(10));
        
        var counter = await _db.Counters.FirstAsync();

        counter.Value++;

        var log = new Log
        {
            Message = $"Новое значение {counter.Value}",
            LoggedAt = DateTimeOffset.UtcNow
        };
        
        _db.Logs.Add(log);
        
        await _db.SaveChangesAsync();
    }
}