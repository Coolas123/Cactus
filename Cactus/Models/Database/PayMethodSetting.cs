using System.ComponentModel.DataAnnotations.Schema;

namespace Cactus.Models.Database
{
    [Table("pay_method_setting")]
    public class PayMethodSetting
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("transaction_type_id")]
        public int TransactionTypeId { get; set; }
        public TransactionType TransactionType { get; set; }
        [Column("comission", TypeName = "decimal(5,2)")]
        public decimal Comission { get; set; }
        [Column("daily_withdraw_limit", TypeName = "decimal(10,2)")]
        public decimal DailyWithdrawLimit { get; set; }
        [Column("monthly_withdraw_limit", TypeName = "decimal(10,2)")]
        public decimal MonthlyWithdrawLimit { get; set; }
    }
}
