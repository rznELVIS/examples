using Microsoft.EntityFrameworkCore;

namespace Getapplock.Data;

public class GetApplockDbContext : DbContext
{
    public GetApplockDbContext(DbContextOptions<GetApplockDbContext> options)
        : base(options)
    {
    }
    
    public GetApplockDbContext()
    {
    }
}