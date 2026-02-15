using Lock.Data;
using Lock.Logic;
using Microsoft.EntityFrameworkCore;

namespace IsolationLevelAccess;

public class ProcessService(LockDbContext db, IDbContextFactory<LockDbContext> dbContextFactory) 
    : BaseProcessService(db, dbContextFactory)
{
    public override async Task DoAsync()
    {
        await using var context = await DbFactory.CreateDbContextAsync();
        
        await DoImplementationAsync(context);
    }
}