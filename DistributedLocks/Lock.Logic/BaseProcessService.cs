using Lock.Data;
using Lock.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace Lock.Logic;

public abstract class BaseProcessService(LockDbContext db)
{
    private readonly LockDbContext _db = db;

    public async Task Clear()
    {
        await _db.Logs.ExecuteDeleteAsync();
        
        var counter = _db.Counters.First();
        
        counter.Value = 0;
        
        await _db.SaveChangesAsync();
    }
    
    protected virtual async Task DoImplementationAsync()
    {
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