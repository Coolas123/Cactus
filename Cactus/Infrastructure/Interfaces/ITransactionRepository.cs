using Cactus.Models.Database;

namespace Cactus.Infrastructure.Interfaces
{
    public interface ITransactionRepository:IBaseRepository<Transaction>
    {
        Task<IEnumerable<Transaction>> GetWidrawAndReplenishAsync(int userId);
        Task<IEnumerable<Transaction>> GetWidrawAndReplenishAsync(int userId, DateTime dateFrom, DateTime dateTo);
    }
}
