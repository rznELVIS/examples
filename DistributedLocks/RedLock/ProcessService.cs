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
        await using (var redLock = await redisService.CreateLockAsync())
        {
            if (redLock.IsAcquired)
            {
                var lockValue = await redisService.GetLockValueAsync();
                Console.WriteLine($"lockValue: {lockValue}");

                await using var context = await dbContextFactory.CreateDbContextAsync();
                await DoImplementationAsync(context);

                //await redisService.SetLockValueAsync(Guid.NewGuid().ToString());

                //lockValue = await redisService.GetLockValueAsync();
                //Console.WriteLine($"changed lockValue: {lockValue}");
            }
            else
            {
                Console.WriteLine("Lock already acquired");
            }
        }
        
        /*var lockValueAfterLock = await redisService.GetLockValueAsync();
        Console.WriteLine($"lockValueAfterLock: {lockValueAfterLock}");*/
    }
}