using Lock.Data;
using Lock.Logic;
using Microsoft.EntityFrameworkCore;

namespace DbLock;

public class ProcessService(
        LockDbContext db, 
        IDbContextFactory<LockDbContext> dbContextFactory) 
    : BaseProcessService(db, dbContextFactory)
{
    public override async Task DoAsync()
    {
        var lockResource = "counter";
        var lockName = Guid.NewGuid().ToString();
        
        await using var context = await DbFactory.CreateDbContextAsync();
        
        var lockResult = await context.Lock(resource:lockResource, lockedBy:lockName);

        if (lockResult == lockName)
        {
            await DoImplementationAsync(context);
        }
    }
}