using System.ComponentModel.DataAnnotations;

namespace Cactus.Models.ViewModels
{
    public class PayMethodSettingViewModel
    {
        [Display(Name = "Комиссия")]
        public decimal Comission { get; set; }
        [Display(Name = "Дневной лимит")]
        public decimal DailyWithdrawLimit { get; set; }
        [Display(Name="Месячный лимит")]
        public decimal MonthlyWithdrawLimit { get; set; }
    }
}
