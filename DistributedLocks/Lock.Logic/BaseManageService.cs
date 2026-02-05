using Microsoft.Extensions.DependencyInjection;

namespace Lock.Logic;

public abstract class BaseManageService
{
    public async Task Do(ServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            await Do(scope);
        }
    }

    public virtual Task Do(IServiceScope scope)
    {
        return null;
    }
}