using LooseEnds.Database;

namespace LooseEnds.Api.Common;

public abstract class BaseService(GameContext context)
{
    protected readonly GameContext _context = context;

    public void SaveContext()
    {
        _context.SaveChanges();
    }

    public async Task SaveContextAsync()
    {
        await _context.SaveChangesAsync();
    }
}
