using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace Cactus.Infrastructure.Repositories
{
    public class DonatorRepository : IDonatorRepository
    {
        private readonly ApplicationDbContext dbContext;
        public DonatorRepository(ApplicationDbContext dbContext) {
            this.dbContext = dbContext;
        }
        public async Task<bool> CreateAsync(Donator entity) {
            await dbContext.Donators.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public Task<bool> DeleteAsync(Donator entity) {
            throw new NotImplementedException();
        }

        public Task<Donator> GetAsync(int id) {
            throw new NotImplementedException();
        }

        public async Task<Dictionary<int, decimal>> GetCollectedSumOfGoalsAsync(List<int> optionId) {
            return await dbContext.Donators
                .Include(x=>x.Transaction)
                .Where(x =>
                    optionId.Contains(x.DonationOptionId))
                .GroupBy(x => new { x.DonationOptionId })
                .Select(x => new KeyValuePair<int, decimal>(x.Key.DonationOptionId, x.Sum(x=>x.Transaction.Received)))
                .ToDictionaryAsync(p => p.Key, p => p.Value);
        }

        public async Task<Donator> GetDonatorAsync(int typeId, int userId) {
            return await dbContext
                .Donators
                .Include(x=>x.DonationOption)
                .FirstOrDefaultAsync(x=>
                    x.DonationTargetTypeId==typeId&&
                    x.UserId== userId
                    );
        }

        public async Task<IEnumerable<Donator>> GetDonatorsAsync(int userId) {
            return await dbContext.Donators
                .Include(x=>x.DonationOption)
                .Include(x=>x.Transaction)
                .Include(x=>x.User)
                .Where(x=>x.DonationOption.AuthorId==userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Donator>> GetDonatorsAsync(int userId, DateTime dateFrom, DateTime dateTo) {
            return await dbContext.Donators
                .Include(x => x.DonationOption)
                .Include(x => x.Transaction)
                .Include(x => x.User)
                .Where(x => 
                        x.DonationOption.AuthorId == userId
                        &&x.Transaction.Created.Date>= dateFrom.Date
                        && x.Transaction.Created.Date <= dateTo.Date)
                .ToListAsync();
        }

        public Task<IEnumerable<Donator>> SelectAsync() {
            throw new NotImplementedException();
        }
    }
}
