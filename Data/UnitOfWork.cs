
using PocketBook.Core.IConfiguration;
using PocketBook.Core.IRepositories;
using PocketBook.Core.Repositiories;

namespace PocketBook.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable 
    {
        private readonly ApplicationDBContext _context;

        private readonly ILogger _logger;

        public IUserRepository Users { get; private set; }

        public UnitOfWork(ApplicationDBContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("log");

            Users = new UserRepository(_context, _logger);

        }
        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
             _context.Dispose();    
        }

    }
}
