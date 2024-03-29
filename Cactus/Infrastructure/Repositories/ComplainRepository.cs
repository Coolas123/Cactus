using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace Cactus.Infrastructure.Repositories
{
    public class ComplainRepository : IComplainRepository
    {
        private readonly ApplicationDbContext dbContext;
        public ComplainRepository(ApplicationDbContext dbContext) {
            this.dbContext = dbContext;
        }

        public async Task<bool> AddComplainAsync(Complain entity) {
            await dbContext.Complains.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public Task<bool> CreateAsync(Complain entity) {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Complain entity) {
            throw new NotImplementedException();
        }

        public Task<Complain> GetAsync(int id) {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Complain>> GetNotReviewedComplainsAsync(DateTime date) {
            return await dbContext.Complains
                .Where(
                    x => x.Created >= date.ToUniversalTime()&&
                    x.ComplainStatusId==(int)Models.Enums.ComplainStatus.NotReviewed
                )
                .ToListAsync();
        }

        public Task<IEnumerable<Complain>> SelectAsync() {
            throw new NotImplementedException();
        }
    }
}
