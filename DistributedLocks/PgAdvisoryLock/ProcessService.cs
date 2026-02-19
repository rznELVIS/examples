using Lock.Data;
using Lock.Data.Data;
using Lock.Logic;
using Microsoft.EntityFrameworkCore;

namespace PgAdvisoryLock.Services;

public class ProcessService(LockDbContext db,
    IDbContextFactory<LockDbContext> dbContextFactory) 
    : BaseProcessService(db, dbContextFactory)
{
    public override Task DoAsync()
    {
        throw new NotImplementedException();
    }
}