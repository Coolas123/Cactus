using Cactus.Models.Database;

namespace Cactus.Infrastructure.Interfaces
{
    public interface IWalletRepository:IBaseRepository<Wallet>
    {
        Task<Wallet> GetWallet(int userId);
    }
}
