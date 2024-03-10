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

        public async Task<Donator> GetDonator(int targetId, int typeId, int userId) {
            return await dbContext
                .Donators
                .Include(x=>x.DonationOption)
                .FirstOrDefaultAsync(x=>
                    x.DonationTargetId== targetId&&
                    x.DonationTargetTypeId==typeId&&
                    x.UserId== userId
                    );
        }

        public Task<IEnumerable<Donator>> SelectAsync() {
            throw new NotImplementedException();
        }
    }
}
