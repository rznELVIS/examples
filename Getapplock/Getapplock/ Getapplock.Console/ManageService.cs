using Getapplock.Data;

namespace Getapplock.Console;

public class ManageService
{
    private readonly GetApplockDbContext _context;

    public ManageService(GetApplockDbContext context)
    {
        _context = context;
    }

    public void Do()
    {
        var counters = _context.Counters.ToList();
    }
}