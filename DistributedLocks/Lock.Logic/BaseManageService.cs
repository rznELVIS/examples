using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace Lock.Logic;

public abstract class BaseManageService(BaseProcessService  service)
{
    public async Task<string> DoWithStatistics(int count)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        
        await Do(count);
        
        stopwatch.Stop();
        
        return $"Общее время: {stopwatch.ElapsedMilliseconds} ms";
    } 
    
    public abstract Task Do(int count);
}