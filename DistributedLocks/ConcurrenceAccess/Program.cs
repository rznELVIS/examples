using ConcurrenceAccess;
using Lock.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

var services = new ServiceCollection();

services
    .AddDbContext<LockDbContext>(options =>
        options .UseNpgsql(Constants.ConnectionString));

services
    .AddDbContextFactory<LockDbContext>(options =>
        options .UseNpgsql(Constants.ConnectionString));

services.AddScoped<ManageService>();
services.AddScoped<ProcessService>();

var serviceProvider = services.BuildServiceProvider();
using (var scope = serviceProvider.CreateScope())
{
    var manager = scope.ServiceProvider.GetRequiredService<ManageService>();
    await manager.Do();
}

Console.WriteLine("Concurrence example is completed.");