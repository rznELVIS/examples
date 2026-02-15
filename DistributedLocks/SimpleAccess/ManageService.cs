using Lock.Logic;
using Microsoft.Extensions.DependencyInjection;

namespace SimpleAccess;

public class ManageService(ProcessService processService) : BaseManageService(processService)
{
    public override async Task Do(int count)
    {
        for (int i = 0; i < count; i++)
        {
            await processService.DoAsync();
        }
    }
}