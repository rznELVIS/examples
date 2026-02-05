using Microsoft.EntityFrameworkCore;
using PgAdvisoryLock.Data;
using PgAdvisoryLock.Data.Dbo;

namespace PgAdvisoryLock.Services;

public class ProcessService
{
    private readonly PgAdvisoryLockDbContext _db;

    public ProcessService(
        PgAdvisoryLockDbContext db)
    {
        _db = db;
    }
    
    public async Task DoAsync(Random random)
    {
        Thread.Sleep(random.Next(10));
        
        var counter = await _db.Users.FirstAsync();

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