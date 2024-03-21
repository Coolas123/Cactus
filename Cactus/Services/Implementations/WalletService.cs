using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;
using Cactus.Services.Interfaces;

namespace Cactus.Services.Implementations
{
    public class WalletService : IWalletService
    {
        private readonly IWalletRepository walletRepository;
        public WalletService(IWalletRepository walletRepository) {
            this.walletRepository = walletRepository;
        }

        public async Task<BaseResponse<Wallet>> GetWallet(int userId) {
            Wallet wallet = await walletRepository.GetWallet(userId);
            if (wallet==null) {
                return new BaseResponse<Wallet>
                {
                    Description=""
                };
            }
            return new BaseResponse<Wallet>
            {
                Data= wallet,
                StatusCode=200
            };
        }

        public async Task<BaseResponse<bool>> AddWallet(WalletViewModel model) {
            Wallet walelt = new Wallet
            {
                UserId= model.UserId,
                CurrencyId= model.CurrencyId,
                Balance= model.Balance,
                IsActive= model.IsActive,
            };
            try {
                await walletRepository.CreateAsync(walelt);
            }
            catch {
                return new BaseResponse<bool>
                {
                    Description = ""
                };
            }
            return new BaseResponse<bool>
            {
                Data=true,
                StatusCode=200
            };
        }
    }
}
