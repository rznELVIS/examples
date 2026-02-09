using System.Diagnostics;
using Lock.Logic;

namespace MutexAccess;

public class ManageService(ProcessService service) : BaseManageService(service)
{
    public override async Task Do(int count)
    {
        var tasks = new List<Task>();
        
        for (int i = 0; i < count; i++)
        {
            Task task = Task.Run(GetTask);
            tasks.Add(task);
        }
        
        await Task.WhenAll(tasks);
    }

    public string DoSync(int count)
    {
        service.ClearSync();
        
        var threads = new List<Thread>();

        Stopwatch stopwatch = Stopwatch.StartNew();
        
        for (int i = 0; i < count; i++)
        {
            Thread thred = new Thread(() => 
            {
                service.Do();
            });
            
            threads.Add(thred);
        }

        foreach (var thread in threads)
        {
            Thread.Sleep(_random.Next(50));
            thread.Start();
        }
        
        foreach (var thread in threads)
        {
            thread.Join();
        }
        
        stopwatch.Stop();

        return $"Общее время: {stopwatch.ElapsedMilliseconds} ms";
    }
}