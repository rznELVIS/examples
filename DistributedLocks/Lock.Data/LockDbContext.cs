using Lock.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace Lock.Data;

public class LockDbContext : DbContext
{
    public DbSet<Counter> Counters { get; set; }
    
    public DbSet<Log> Logs { get; set; }
    
    
    public DbSet<Locker> Locks { get; set; }
    
    public LockDbContext(DbContextOptions<LockDbContext> options)
        : base(options)
    {
    }
    
    public LockDbContext()
    {
    }

    public Task<string> CreateLock(string resource, string lockedBy)
    {
        var sql = @"
            INSERT INTO lock (resource, locked_by, expires_at)
            VALUES ({0}, {1}, NOW() + ({2} || ' seconds')::interval)
            ON CONFLICT (resource) DO UPDATE
            SET locked_by = {1},
                expires_at = NOW() + ({2} || ' seconds')::interval
            WHERE lock.expires_at < NOW()
            RETURNING locked_by;";
        
        var result =  Database
            .SqlQueryRaw<string>(sql, resource, lockedBy, 10)
            .AsEnumerable();

        foreach (var item in result)
        {
            return Task.FromResult(item);
        }

        return Task.FromResult(string.Empty);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Counter>(entity =>
        {
            entity.ToTable("counter");
            entity.HasKey(x => x.Id);

            entity
                .Property(x => x.Id)
                .HasColumnName("id")
                .IsRequired();

            entity
                .Property(x => x.Value)
                .HasColumnName("value")
                .IsRequired();
        });

        modelBuilder.Entity<Log>(entity =>
        {
            entity.ToTable("log");
            entity.HasKey(x => x.Id);

            entity
                .Property(x => x.Id)
                .HasColumnName("id")
                .IsRequired();

            entity
                .Property(x => x.Message)
                .HasColumnName("message")
                .IsRequired(false);

            entity
                .Property(x => x.LoggedAt)
                .HasColumnName("logged_at")
                .IsRequired(false);
        });

        modelBuilder.Entity<Locker>(entity =>
        {
            entity.ToTable("lock");
            entity.HasKey(x => x.Resource);

            entity
                .Property(x => x.Resource)
                .HasColumnName("resource")
                .IsRequired();

            entity
                .Property(x => x.LockedBy)
                .HasColumnName("locked_by")
                .IsRequired(false);
            
            entity
                .Property(x => x.ExpiresAt)
                .HasColumnName("expires_at")
                .IsRequired(false);
        });
    }
}