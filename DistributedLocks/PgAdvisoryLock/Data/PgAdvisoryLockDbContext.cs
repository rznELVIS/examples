using Microsoft.EntityFrameworkCore;
using PgAdvisoryLock.Data.Dbo;

namespace PgAdvisoryLock.Data;

public class PgAdvisoryLockDbContext : DbContext
{
    public DbSet<Counter> Users { get; set; }
    
    public DbSet<Log> Logs { get; set; }
    
    public PgAdvisoryLockDbContext(DbContextOptions<PgAdvisoryLockDbContext> options)
        : base(options)
    {
    }
    
    public PgAdvisoryLockDbContext()
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
    }
}