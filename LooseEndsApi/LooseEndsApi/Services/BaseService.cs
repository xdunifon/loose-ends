namespace LooseEndsApi.Services
{
    public abstract class BaseService
    {
        protected readonly GameDbContext _context;

        public BaseService(GameDbContext context)
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
}
