using Lock.Logic;

namespace LockAccess;

public class ManageService(ProcessService processService) : BaseManageService
{
    private readonly ProcessService _processService = processService;
    private readonly Random _random = new();
    
    public override async Task Do()
    {
        await _processService.Clear();
        
        var tasks = new List<Task>();
        
        for (int i = 0; i < 100; i++)
        {
            Task task = Task.Run(GetTask);
            tasks.Add(task);
        }
        
        await Task.WhenAll(tasks);
    }
    
    private Task GetTask()
    {
        Thread.Sleep(_random.Next(50));
        return _processService.DoAsync();
    }
}