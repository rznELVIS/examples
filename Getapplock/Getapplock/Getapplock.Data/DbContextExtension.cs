using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Getapplock.Data;

public static class DbContextExtension
{
    public static void AddBaseDbContext(this ServiceCollection services)
    {
        services
            .AddDbContextFactory<GetApplockDbContext>(options =>
                options.UseSqlServer(Constants.ConnectionString));
    }
}