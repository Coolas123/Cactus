using Cactus.Models.Database;

namespace Cactus.Models.ViewModels
{
    public class WalletSettingViewModel
    {
        public IEnumerable<PayMethod> PayMethods { get; set; }
        public Wallet Wallet { get; set; }
        public TransactionViewModel Replenish { get; set; }
        public TransactionViewModel Withdraw { get; set; }
    }
}
