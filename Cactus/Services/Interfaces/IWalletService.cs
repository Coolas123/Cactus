using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;

namespace Cactus.Services.Interfaces
{
    public interface IWalletService
    {
        Task<BaseResponse<Wallet>> GetWallet(int userId);
        Task<BaseResponse<bool>> AddWallet(WalletViewModel model);
        Task<BaseResponse<Wallet>> ReplenishWallet(int userId, decimal sum);
    }
}
