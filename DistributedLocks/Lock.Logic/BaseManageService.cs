using Microsoft.Extensions.DependencyInjection;

namespace Lock.Logic;

public abstract class BaseManageService()
{
    public abstract Task Do();
}