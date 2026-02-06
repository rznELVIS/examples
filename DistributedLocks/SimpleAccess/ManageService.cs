using Lock.Logic;
using Microsoft.Extensions.DependencyInjection;

namespace SimpleAccess;

public class ManageService(ProcessService processService) : BaseManageService()
{
    public override async Task Do()
    {
        await processService.Clear();
        
        for (int i = 0; i < 100; i++)
        {
            await processService.DoAsync();
        }
    }
}