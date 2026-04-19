using Getapplock.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Getapplock.Data;

public class GetApplockDbContext : DbContext
{
    public DbSet<Counter> Counters { get; set; }
    
    public DbSet<Log> Logs { get; set; }
    
    public GetApplockDbContext(DbContextOptions<GetApplockDbContext> options)
        : base(options)
    {
    }
    
    public GetApplockDbContext()
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