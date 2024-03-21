using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace Cactus.Infrastructure.Repositories
{
    public class WalletRepository : IWalletRepository
    {
        private readonly ApplicationDbContext dbContext;
        public WalletRepository(ApplicationDbContext dbContext) {
            this.dbContext = dbContext;
        }

        public async Task<bool> CreateAsync(Wallet entity) {
            await dbContext.Wallets.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public Task<bool> DeleteAsync(Wallet entity) {
            throw new NotImplementedException();
        }

        public Task<Wallet> GetAsync(int id) {
            throw new NotImplementedException();
        }

        public async Task<Wallet> GetWallet(int userId) {
            return await dbContext.Wallets.Include(x=>x.Currency).FirstOrDefaultAsync(x=>x.UserId==userId);
        }

        public Task<IEnumerable<Wallet>> SelectAsync() {
            throw new NotImplementedException();
        }
    }
}
