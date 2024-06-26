﻿using Cactus.Models.Database;

namespace Cactus.Infrastructure.Interfaces
{
    public interface IWalletRepository:IBaseRepository<Wallet>
    {
        Task<Wallet> GetWalletAsync(int userId);
        Task<bool> UpdateAsync(Wallet wallet);
    }
}
