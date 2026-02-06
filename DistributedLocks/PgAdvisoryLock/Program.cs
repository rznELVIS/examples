using Lock.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PgAdvisoryLock.Services;

var services = new ServiceCollection();

services
    .AddDbContext<LockDbContext>(options =>
        options .UseNpgsql(Constants.ConnectionString),
        ServiceLifetime.Transient);
services.AddTransient<ProcessService>();
services.AddScoped<ManagerService>();
services.AddScoped<ConcurrentManagerService>();

var serviceProvider = services.BuildServiceProvider();

using (var scope = serviceProvider.CreateScope())
{
    var manager = scope.ServiceProvider.GetRequiredService<ConcurrentManagerService>();

    await manager.Do(serviceProvider);
}

Console.WriteLine("Completed");