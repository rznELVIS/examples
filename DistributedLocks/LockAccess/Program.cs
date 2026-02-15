using Lock.Logic;
using LockAccess;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

services.AddBaseDbContext();

services.AddScoped<ManageService>();
services.AddScoped<ProcessService>();

var serviceProvider = services.BuildServiceProvider();
using (var scope = serviceProvider.CreateScope())
{
    var manager = scope.ServiceProvider.GetRequiredService<ManageService>();
    var result = await manager.DoWithStatistics(LogicConstants.BaseCount);
     
    Console.WriteLine(result);
}

Console.WriteLine("Lock example is completed.");