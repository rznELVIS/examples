using ConcurrenceAccess;
using Lock.Data;
using Lock.Logic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

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

Console.WriteLine("Concurrence example is completed.");