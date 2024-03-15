using System.ComponentModel.DataAnnotations.Schema;

namespace Cactus.Models.Database
{
    [Table("pay_method_setting_id")]
    public class PayMethodSetting
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("comission")]
        public decimal Comission { get; set; }
        [Column("daily_withdraw_limit")]
        public decimal DailyWithdrawLimit { get; set; }
        [Column("monthly_withdraw_limit")]
        public decimal MonthlyWithdrawLimit { get; set; }
    }
}
