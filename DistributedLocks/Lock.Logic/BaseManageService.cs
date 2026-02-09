using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace Lock.Logic;

public abstract class BaseManageService(BaseProcessService service)
{
    protected readonly Random _random = new();
    
    public async Task<string> DoWithStatistics(int count)
    {
        await service.Clear();
        
        Stopwatch stopwatch = Stopwatch.StartNew();
        
        await Do(count);
        
        stopwatch.Stop();
        
        return $"Общее время: {stopwatch.ElapsedMilliseconds} ms";
    } 
    
    public abstract Task Do(int count);
    
    protected virtual Task GetTask()
    {
        Thread.Sleep(_random.Next(50));
        return service.DoAsync();
    }
}