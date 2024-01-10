using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace Cactus.Infrastructure.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly ApplicationDbContext dbContext;
        public UserRepository(ApplicationDbContext dbContext) {
            this.dbContext = dbContext;
        }
        public async Task<bool> CreateAsync(User entity)
        {
            await dbContext.Users.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(User id)
        {
            dbContext.Users.Remove(id);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<User> GetAsync(int id)
        {
            return await dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<IEnumerable<User>> SelectAsync()
        {
            return await dbContext.Users.ToListAsync();
        }
    }
}
