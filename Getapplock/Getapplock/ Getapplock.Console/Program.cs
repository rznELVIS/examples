using Getapplock.Console;
using Getapplock.Data;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

services.AddBaseDbContext();

services.AddScoped<ManageService>();

var serviceProvider = services.BuildServiceProvider();

using (var scope = serviceProvider.CreateScope())
{
    var manager = scope.ServiceProvider.GetRequiredService<ManageService>();
    manager.Do();
}

Console.WriteLine("Hello, World!");