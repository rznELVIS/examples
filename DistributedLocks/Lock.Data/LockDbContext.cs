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