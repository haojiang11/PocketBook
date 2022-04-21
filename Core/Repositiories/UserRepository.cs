using Microsoft.EntityFrameworkCore;
using PocketBook.Core.IRepositories;
using PocketBook.Data;

namespace PocketBook.Core.Repositiories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDBContext context, ILogger logger) : base(context, logger)
        {

        }

        public override async Task<IEnumerable<User>> All() 
        {
            try { 
                return await dbSet.ToListAsync();    
            }
            catch ( Exception e) {
                _logger.LogError(e, "{Repo} All method error", typeof(UserRepository));
                return new List<User>();    
            }

        }
        public override async Task<bool> Upsert(User entity)
        {
            var existingUser = await dbSet.Where(x => x.Id == entity.Id).FirstOrDefaultAsync();

            if (existingUser == null)
            {
                return await Add(entity);
            }

            try
            {
                existingUser.FirstName = entity.FirstName;
                existingUser.LastName = entity.LastName;
                existingUser.Email = entity.Email;
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{Repo} Upsert method error", typeof(UserRepository));
                return false;
            }
        }

        public override async Task<bool> Delete(Guid id)
        {
            try
            {
                var existingUser = await dbSet.Where(x => x.Id == id).FirstOrDefaultAsync();

                if (existingUser != null)
                {
                    dbSet.Remove(existingUser);
                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{Repo} Delete method error", typeof(UserRepository));
                return false;
            }
        }

    }
}
