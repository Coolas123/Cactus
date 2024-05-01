using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cactus.Models.Database
{
    [Table("wallet")]
    public class Wallet
    {
        [Key]
        [ForeignKey("User")]
        [Column("user_id")]
        public int UserId { get; set; }
        public User User { get; set; }
        [Column("currency_id")]
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }
        [Column("balance", TypeName = "decimal(10,2)")]
        public decimal Balance { get; set; }
        [Column("is_active")]
        public bool IsActive { get; set; }
    }
}
