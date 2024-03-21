using Cactus.Models.Database;
using System.ComponentModel.DataAnnotations;

namespace Cactus.Models.ViewModels
{
    public class WalletViewModel
    {
        public int UserId { get; set; }
        [Display(Name ="Валюта")]
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }
        public decimal Balance { get; set; }
        public bool IsActive { get; set; }
    }
}
