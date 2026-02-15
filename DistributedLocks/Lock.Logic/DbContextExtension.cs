using Lock.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lock.Logic;

public static class DbContextExtension
{
    public static void AddBaseDbContext(this ServiceCollection services)
    {
        services
            .AddDbContext<LockDbContext>(options =>
                options .UseNpgsql(Constants.ConnectionString));

        services
            .AddDbContextFactory<LockDbContext>(options =>
                options .UseNpgsql(Constants.ConnectionString));
    }
}