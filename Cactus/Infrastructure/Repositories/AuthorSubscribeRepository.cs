using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace Cactus.Infrastructure.Repositories
{
    public class AuthorSubscribeRepository : IAuthorSubscribeRepository
    {
        private readonly ApplicationDbContext dbContext;
        public AuthorSubscribeRepository(ApplicationDbContext dbContext) {
            this.dbContext = dbContext;
        }

        public async Task<AuthorSubscribe> GetSubscriptionAsync(int subcriberId, int authorId) {
            return await dbContext.AuthorSubscribes.FirstOrDefaultAsync(x=>x.UserId== subcriberId && x.AuthorId== authorId);
        }

        public async Task<bool> CreateAsync(AuthorSubscribe entity) {
            await dbContext.AuthorSubscribes.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(AuthorSubscribe entity) {
            dbContext.Remove(entity);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public Task<AuthorSubscribe> GetAsync(int id) {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AuthorSubscribe>> SelectAsync() {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AuthorSubscribe>> GetPagingSubscribersAsync(int subscriberId, int authorPage, int pageSize) {
            return await dbContext.AuthorSubscribes.Include(x=>x.Author).Where(x => x.UserId == subscriberId).OrderBy(x => x.Author.UserName).Skip((authorPage - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<IEnumerable<AuthorSubscribe>> GetSubscribersAsync(int subcriberId) {
            return await dbContext.AuthorSubscribes.Where(x=>x.UserId==subcriberId).ToListAsync();
        }

        public async Task<AuthorSubscribe> GetSubscribeAsync(int userId, int authorId) {
            return await dbContext.AuthorSubscribes.FirstOrDefaultAsync(x=>x.AuthorId== authorId&&x.UserId==userId);
        }
    }
}
