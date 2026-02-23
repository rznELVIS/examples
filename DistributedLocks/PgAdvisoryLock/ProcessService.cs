using Lock.Data;
using Lock.Data.Data;
using Lock.Logic;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace PgAdvisoryLock;

public class ProcessService(LockDbContext db,
    IDbContextFactory<LockDbContext> dbContextFactory) 
    : BaseProcessService(db, dbContextFactory)
{
    private class LockResult
    {
        public bool acquired { get; set; }
    }
    
    public override async Task DoAsync()
    {
        var resource = 123;

        await using var context = await dbContextFactory.CreateDbContextAsync();
        
        await using var transaction = await context.Database.BeginTransactionAsync();
        
        var result = await context
            .Database
            .SqlQuery<LockResult>($"SELECT pg_try_advisory_xact_lock({resource}) as acquired")
            .SingleAsync();

        if (result.acquired)
        {
            try
            {
                await DoImplementationAsync(context);
            }
            finally
            {
                await transaction.CommitAsync();
            }
        }
    }
}