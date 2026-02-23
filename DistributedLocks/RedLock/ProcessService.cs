using Lock.Data;
using Lock.Logic;
using Microsoft.EntityFrameworkCore;

namespace RedLock;

public class ProcessService(LockDbContext db,
    IDbContextFactory<LockDbContext> dbContextFactory,
    RedisService redisService) 
    : BaseProcessService(db, dbContextFactory)
{
    public override async Task DoAsync()
    {
        await using var context = await dbContextFactory.CreateDbContextAsync();
        
        await DoImplementationAsync(context);
    }
}