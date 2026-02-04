// See https://aka.ms/new-console-template for more information

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PgAdvisoryLock.Data;

var services = new ServiceCollection();

services
    .AddDbContext<PgAdvisoryLockDbContext>(options =>
        options.UseNpgsql("Host=localhost;Port=5432;Database=pg_advisory_lock_db;Username=user;Password=password"));

var serviceProvider = services.BuildServiceProvider();

// Использование
using (var scope = serviceProvider.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PgAdvisoryLockDbContext>();
}

Console.WriteLine("Hello, World!");