using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Cactus.Models.Database
{
    [Table("goal")]
    public class Goal
    {
        [Key]
        [ForeignKey("DonationOption")]
        [Column("donation_option_id")]
        public int DonationOptionId { get; set; }
        public DonationOption DonationOption { get; set; }
        [Column("total_amount", TypeName = "decimal(10,2)")]
        public decimal TotalAmount { get; set; }
        [Column("is_done")]
        public bool IsDone { get; set; }
    }
}
