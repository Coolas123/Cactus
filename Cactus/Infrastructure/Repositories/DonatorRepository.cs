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
        public Task<bool> CreateAsync(Donator entity) {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Donator entity) {
            throw new NotImplementedException();
        }

        public Task<Donator> GetAsync(int id) {
            throw new NotImplementedException();
        }

        public async Task<Dictionary<int, decimal>> GetCollectedSumOfGoalsAsync(List<int> optionId) {
            return await dbContext.Donators.Where(x =>
                    optionId.Contains(x.DonationOptionId))
                .GroupBy(x => new { x.DonationOptionId })
                .Select(x => new KeyValuePair<int, decimal>(x.Key.DonationOptionId, x.Sum(x=>x.TotalAmount)))
                .ToDictionaryAsync(p => p.Key, p => p.Value);
        }

        public async Task<Donator> GetDonatorAsync(int targetId, int typeId, int userId) {
            return await dbContext
                .Donators
                .Include(x=>x.DonationOption)
                .FirstOrDefaultAsync(x=>
                    x.DonationTargetId== targetId&&
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

        public Task<IEnumerable<Donator>> SelectAsync() {
            throw new NotImplementedException();
        }
    }
}
