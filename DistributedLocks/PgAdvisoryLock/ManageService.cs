using Lock.Logic;
using Microsoft.Extensions.DependencyInjection;

namespace PgAdvisoryLock.Services;

public class ManageService(ProcessService service) : BaseManageService(service)
{
    public override Task Do(int count)
    {
        throw new NotImplementedException();
    }
}