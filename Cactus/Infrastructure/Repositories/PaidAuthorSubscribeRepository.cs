using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IEnumerable<PaidAuthorSubscribe>> GetCurrentSubscribes(int authorId, int donatorId) {
            return await dbContext.PaidAuthorSubscribes
                .Include(x=>x.Donator)
                .ThenInclude(x=>x.User)
                .Include(x => x.Donator)
                .ThenInclude(x => x.DonationOption)
                .Where(x=>x.DonatorId== donatorId && 
                x.EndDate.ToLocalTime()>=DateTime.Now)
                .ToListAsync();
        }

        public Task<IEnumerable<PaidAuthorSubscribe>> SelectAsync() {
            throw new NotImplementedException();
        }

        public async Task<bool> SubscribeToAuthorAsync(PaidAuthorSubscribe entity) {
            await dbContext.PaidAuthorSubscribes.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return true;
        }
    }
}
