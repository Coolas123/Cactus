using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace Cactus.Infrastructure.Repositories
{
    public class PayMethodRepository : IPayMethodRepository
    {
        private readonly ApplicationDbContext dbContext;
        public PayMethodRepository(ApplicationDbContext dbContext) {
            this.dbContext = dbContext;
        }

        public async Task<bool> CreateAsync(PayMethod entity) {
            await dbContext.PayMethods.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public Task<bool> DeleteAsync(PayMethod entity) {
            throw new NotImplementedException();
        }

        public Task<PayMethod> GetAsync(int id) {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PayMethod>> GetReplenishMethods() {
            return await dbContext.PayMethods.Include(x=>x.PayMethodSetting).Where(x=>x.PayMethodSetting.TransactionTypeId == (int)Models.Enums.TransactionType.Replenish).ToListAsync();
        }

        public Task<IEnumerable<PayMethod>> SelectAsync() {
            throw new NotImplementedException();
        }
    }
}
