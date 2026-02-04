using Microsoft.EntityFrameworkCore;
using PgAdvisoryLock.Data.Dbo;

namespace PgAdvisoryLock.Data;

public class PgAdvisoryLockDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    
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

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users");
            entity.HasKey(x => x.Id);

            entity
                .Property(x => x.Id)
                .HasColumnName("id")
                .IsRequired();

            entity
                .Property(x => x.Name)
                .HasColumnName("name")
                .IsRequired();

            entity
                .Property(x => x.Email)
                .HasColumnName("email")
                .IsRequired();
        });
    }
}