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
}