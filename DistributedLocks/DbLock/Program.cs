using DbLock;
using Lock.Logic;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

services.AddBaseDbContext();

services.AddScoped<ProcessService>();
services.AddScoped<ManageService>();

using (var scope = services.BuildServiceProvider().CreateScope())
{
    
}

Console.WriteLine("Db example is completed.");