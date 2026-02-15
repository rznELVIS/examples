using Lock.Data;
using Lock.Logic;
using Microsoft.EntityFrameworkCore;

namespace DbLock;

public class ProcessService(
        LockDbContext db, 
        IDbContextFactory<LockDbContext> dbContextFactory) 
    : BaseProcessService(db, dbContextFactory)
{
    private Random _random = new();
    
    public override async Task DoAsync()
    {
        var lockResource = "counter";
        var lockName = Guid.NewGuid().ToString();
        
        await using var context = await DbFactory.CreateDbContextAsync();

        string lockResult;
        var count = 20;

        do
        {
            // получение блокировки
            lockResult = await context.CreateLock(resource: lockResource, lockedBy: lockName);

            if (!string.IsNullOrEmpty(lockResult))
            {
                //Console.WriteLine($"Секция захвачена: {lockName}.");

                await DoImplementationAsync(context);

                // снятие блокировки
                await context
                    .Locks
                    .Where(x => x.Resource == lockResource && x.LockedBy == lockName)
                    .ExecuteDeleteAsync();
            }
            else
            {
                //Console.WriteLine($"Секция заблокирована для {lockName}.");
                
                Thread.Sleep(_random.Next(200));
                count--;
            }
            
        } while (string.IsNullOrEmpty(lockResult) && count > 0);
    }
}