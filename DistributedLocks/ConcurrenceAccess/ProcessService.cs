using Lock.Data;
using Lock.Logic;
using Microsoft.EntityFrameworkCore;

namespace ConcurrenceAccess;

public class ProcessService(LockDbContext db, IDbContextFactory<LockDbContext> dbContextFactory) : BaseProcessService(db, dbContextFactory)
{
    public override async Task DoAsync()
    {
        await using var context = await dbContextFactory.CreateDbContextAsync();
        
        await DoImplementationAsync(context);
    }
}