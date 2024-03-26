using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;

namespace Cactus.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<BaseResponse<bool>> CreateTransaction(TransactionViewModel model);
        Task<BaseResponse<IEnumerable<Transaction>>> GetWidrawAndReplenishAsync(int userId);
    }
}
