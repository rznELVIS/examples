using Lock.Data;
using Lock.Logic;
using Microsoft.EntityFrameworkCore;

namespace SimpleAccess;

public class ProcessService(
    LockDbContext db,
    IDbContextFactory<LockDbContext> dbContextFactory) : BaseProcessService(db, dbContextFactory)
{
    public override async Task DoAsync()
    {
        await DoImplementationAsync(Db);
    }
}