using Lock.Logic;
using Microsoft.Extensions.DependencyInjection;
using SimpleAccess;

var services = new ServiceCollection();

services.AddBaseDbContext();

services.AddScoped<ManageService>();
services.AddScoped<ProcessService>();

var serviceProvider = services.BuildServiceProvider();

using (var scope = serviceProvider.CreateScope())
{
    var manager = scope.ServiceProvider.GetRequiredService<ManageService>();
    await manager.Do();
}

Console.WriteLine("Simple example is completed.");