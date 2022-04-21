using Microsoft.EntityFrameworkCore;

namespace PocketBook.Data
{
    public class ApplicationDBContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
    }
}
