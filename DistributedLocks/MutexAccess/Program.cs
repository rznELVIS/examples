using Lock.Logic;
using Microsoft.Extensions.DependencyInjection;
using MutexAccess;

var services = new ServiceCollection();

services.AddBaseDbContext();

services.AddScoped<ManageService>();
services.AddScoped<ProcessService>();

services.AddSingleton<MutexService>();

var serviceProvider = services.BuildServiceProvider();

using (var scope = serviceProvider.CreateScope())
{
    var manager = scope.ServiceProvider.GetRequiredService<ManageService>();
    var result = manager.DoSync(LogicConstants.BaseCount);
    
    Console.WriteLine(result);
}

Console.WriteLine("Mutex example is completed.");