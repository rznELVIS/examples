using Lock.Logic;
using Microsoft.Extensions.DependencyInjection;

namespace SimpleAccess;

public class ManageService : BaseManageService
{
    public override async Task Do(IServiceScope scope)
    {
        var process = scope.ServiceProvider.GetRequiredService<ProcessService>();

        await process.Clear();
        
        for (int i = 0; i < 100; i++)
        {
            await process.DoAsync();
        }
    }
}