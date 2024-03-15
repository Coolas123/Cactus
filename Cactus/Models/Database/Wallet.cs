using Microsoft.EntityFrameworkCore;
using Nest;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cactus.Models.Database
{
    [Table("wallet")]
    public class Wallet
    {
        [Key]
        [Column("user_id")]
        public int UserId { get; set; }
        public User User { get; set; }
        [Column("currency_id")]
        public int CurrencyId { get; set; }
        [Column("balance")]
        public decimal Balance { get; set; }
        [Column("is_active")]
        public bool IsActive { get; set; }
    }
}
