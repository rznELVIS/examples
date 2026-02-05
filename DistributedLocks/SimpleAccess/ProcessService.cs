using Lock.Data;
using Lock.Logic;

namespace SimpleAccess;

public class ProcessService(LockDbContext db) : BaseProcessService(db)
{
    public async Task DoAsync()
    {
        await DoImplementationAsync();
    }
}