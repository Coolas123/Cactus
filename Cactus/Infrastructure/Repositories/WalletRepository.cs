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

        public async Task<Wallet> GetAsync(int id) {
            return await dbContext.Wallets.FirstOrDefaultAsync(x=>x.UserId==id);
        }

        public async Task<Wallet> GetWallet(int userId) {
            return await dbContext.Wallets.Include(x=>x.Currency).FirstOrDefaultAsync(x=>x.UserId==userId);
        }

        public async Task<bool> UpdateAsync(Wallet wallet) {
            dbContext.Wallets.Update(wallet);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public Task<IEnumerable<Wallet>> SelectAsync() {
            throw new NotImplementedException();
        }

        public Task<bool> WithdrawAsync(Wallet wallet) {
            throw new NotImplementedException();
        }
    }
}
