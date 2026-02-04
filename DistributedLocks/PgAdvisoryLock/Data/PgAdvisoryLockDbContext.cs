using Microsoft.EntityFrameworkCore;

namespace PgAdvisoryLock.Data;

public class PgAdvisoryLockDbContext : DbContext
{
    public PgAdvisoryLockDbContext(DbContextOptions<PgAdvisoryLockDbContext> options)
        : base(options)
    {
    }
    
    public PgAdvisoryLockDbContext()
    {
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }
}