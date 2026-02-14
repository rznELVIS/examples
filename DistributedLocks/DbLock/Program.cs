using DbLock;
using Lock.Logic;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

services.AddBaseDbContext();

services.AddScoped<ManageService>();
services.AddScoped<ProcessService>();

using (var scope = services.BuildServiceProvider().CreateScope())
{
    var manager = scope.ServiceProvider.GetRequiredService<ManageService>();
    var result = await manager.DoWithStatistics(LogicConstants.BaseCount);
    
    Console.WriteLine(result);
}

Console.WriteLine("Db example is completed.");