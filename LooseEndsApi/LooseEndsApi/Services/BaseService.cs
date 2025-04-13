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
            try
            {
                _context.SaveChanges();
            } catch (Exception ex)
            {
                throw;
            }
        }

        public async Task SaveContextAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            } catch (Exception ex)
            {
                throw;
            }
        }
    }
}
