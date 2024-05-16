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

        public async Task<bool> DeleteAsync(User entity)
        {
            dbContext.Users.Remove(entity);
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

        public async Task<User> GetByUserNameAsync(string userName)
        {
            return await dbContext.Users.FirstOrDefaultAsync(x=>x.UserName==userName);
        }

        public async Task<bool> UpdateAsync(User user) {
            dbContext.Users.Update(user);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<User> GetByDateOfBirthAsync(DateTime dateOfBirth) {
            return await dbContext.Users.FirstOrDefaultAsync(x=>x.DateOfBirth==dateOfBirth);
        }
    }
}
