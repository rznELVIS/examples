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
        
        // получение блокировки
        var lockResult = await context.CreateLock(resource:lockResource, lockedBy:lockName);

        if (lockResult == lockName)
        {
            Console.WriteLine($"Секция захвачена: {lockName}.");
            
            await DoImplementationAsync(context);

            // снятие блокировки
            await context
                .Locks
                .Where(x => x.Resource == lockResource && x.LockedBy == lockName)
                .ExecuteDeleteAsync();
        }
        else
        {
            Console.WriteLine($"Секция заблокирована для {lockName} заблокирована. Секция заблокированая {lockResult}.");
        }
    }
}