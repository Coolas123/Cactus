using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Cactus.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using SportsStore.Models;

namespace Cactus.Infrastructure.Repositories
{
    public class SubscribeRepository : ISubscribeRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SubscribeRepository(ApplicationDbContext dbContext) {
            this.dbContext = dbContext;
        }

        public async Task<AuthorSubscribe> GetSubscriptionAsync(int subcriberId, int authorId) {
            return await dbContext.AuthorSubscribes.FirstOrDefaultAsync(x=>x.UserId== subcriberId && x.AuthorId== authorId);
        }

        public async Task<bool> CreateAsync(AuthorSubscribe entity) {
            await dbContext.AuthorSubscribes.AddAsync(entity);
            return true;
        }

        public Task<bool> DeleteAsync(AuthorSubscribe entity) {
            throw new NotImplementedException();
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
    }
}
