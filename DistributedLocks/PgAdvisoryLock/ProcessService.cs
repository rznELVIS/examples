using Lock.Data;
using Lock.Data.Data;
using Lock.Logic;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace PgAdvisoryLock.Services;

public class ProcessService(LockDbContext db,
    IDbContextFactory<LockDbContext> dbContextFactory) 
    : BaseProcessService(db, dbContextFactory)
{
    private class LockResult
    {
        public bool Acquired { get; set; }
    }
    
    public override async Task DoAsync()
    {
        var resource = 123;
        
        await using var context = await dbContextFactory.CreateDbContextAsync();
        
        await context.Database.OpenConnectionAsync();
        var connection = context.Database.GetDbConnection();

        await using var command = connection.CreateCommand();
        command.CommandText = "SELECT pg_try_advisory_lock(@key)";
        command.Parameters.Add(new NpgsqlParameter("key", resource));

        var result = (bool?)await command.ExecuteScalarAsync();
        
        Console.WriteLine(result);

        if (result.HasValue && result.Value)
        {
            try
            {
                await DoImplementationAsync(context);
            }
            finally
            {
                /*await using var command1 = connection.CreateCommand();
                
                command1.CommandText = "SELECT pg_advisory_unlock(@key)";
                command1.Parameters.Add(new NpgsqlParameter("key", resource));
                
                await command1.ExecuteScalarAsync();*/
                /*await context.Database
                    .ExecuteSqlRawAsync("SELECT pg_advisory_unlock({0}) as value", resource);*/
            }
        }
    }
}