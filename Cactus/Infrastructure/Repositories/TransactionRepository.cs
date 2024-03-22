using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;

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
    }
}
