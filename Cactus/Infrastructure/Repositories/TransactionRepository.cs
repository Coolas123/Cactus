using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace Cactus.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext dbContext;
        public TransactionRepository(ApplicationDbContext dbContext) {
            this.dbContext = dbContext;
        }

        public async Task<bool> CreateAsync(Transaction entity) {
            await dbContext.Transactions.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public Task<bool> DeleteAsync(Transaction entity) {
            throw new NotImplementedException();
        }

        public Task<Transaction> GetAsync(int id) {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Transaction>> SelectAsync() {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Transaction>> GetWidrawAndReplenishAsync(int userId) {
            return await dbContext.Transactions
                .Include(x => x.PayMethod)
                .Include(x => x.PayMethod.PayMethodSetting)
                .Include(x => x.PayMethod.PayMethodSetting.TransactionType)
                .Where(x =>x.UserId==userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Transaction>> GetWidrawAndReplenishAsync(int userId, DateTime dateFrom, DateTime dateTo) {
            return await dbContext.Transactions
                .Include(x => x.PayMethod)
                .Include(x => x.PayMethod.PayMethodSetting)
                .Include(x => x.PayMethod.PayMethodSetting.TransactionType)
                .Where(x => 
                        x.UserId == userId 
                        && x.Created.Date >= dateFrom.Date 
                        && x.Created.Date <= dateTo.Date)
                .ToListAsync();
        }

        public async Task<Transaction> GetLastTransaction(int userId, DateTime date) {
            return await dbContext.Transactions.FirstOrDefaultAsync(x=>x.UserId== userId && x.Created==date);
        }
    }
}
