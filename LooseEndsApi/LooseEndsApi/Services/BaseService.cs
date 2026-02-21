namespace LooseEndsApi.Services;

public abstract class BaseService
{
    protected readonly GameContext _context;

    public BaseService(GameContext context)
    {
        _context = context;
    }

    public void SaveContext()
    {
        _context.SaveChanges();
    }

    public async Task SaveContextAsync()
    {
        await _context.SaveChangesAsync();
    }
}
