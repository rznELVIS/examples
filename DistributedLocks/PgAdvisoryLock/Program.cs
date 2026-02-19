using Lock.Data;
using Lock.Logic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PgAdvisoryLock.Services;

var services = new ServiceCollection();

services.AddBaseDbContext();

services.AddScoped<ProcessService>();
services.AddScoped<ManageService>();

var serviceProvider = services.BuildServiceProvider();

using (var scope = serviceProvider.CreateScope())
{
    var manager = scope.ServiceProvider.GetRequiredService<ManageService>();
    var result = await manager.DoWithStatistics(LogicConstants.BaseCount);
    
    Console.WriteLine(result);
}

Console.WriteLine("Completed");