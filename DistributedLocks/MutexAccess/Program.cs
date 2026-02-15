using Lock.Logic;
using Microsoft.Extensions.DependencyInjection;
using MutexAccess;

var services = new ServiceCollection();

services.AddBaseDbContext();

services.AddScoped<ManageServiceThreads>();
services.AddScoped<ProcessService>();

services.AddSingleton<MutexService>();

var serviceProvider = services.BuildServiceProvider();

using (var scope = serviceProvider.CreateScope())
{
    var manager = scope.ServiceProvider.GetRequiredService<ManageServiceThreads>();
    var result = manager.DoThreads(LogicConstants.BaseCount);
    
    Console.WriteLine(result);
}

Console.WriteLine("Mutex example is completed.");