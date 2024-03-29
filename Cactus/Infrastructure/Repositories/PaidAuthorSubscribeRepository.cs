using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;

namespace Cactus.Infrastructure.Repositories
{
    public class PaidAuthorSubscribeRepository : IPaidAuthorSubscribeRepository
    {
        private readonly ApplicationDbContext dbContext;
        public PaidAuthorSubscribeRepository(ApplicationDbContext dbContext) {
            this.dbContext = dbContext;
        }

        public Task<bool> CreateAsync(PaidAuthorSubscribe entity) {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(PaidAuthorSubscribe entity) {
            throw new NotImplementedException();
        }

        public Task<PaidAuthorSubscribe> GetAsync(int id) {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PaidAuthorSubscribe>> SelectAsync() {
            throw new NotImplementedException();
        }

        public async Task<bool> SubscribeToAuthor(PaidAuthorSubscribe entity) {
            await dbContext.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return true;
        }
    }
}
