using System.ComponentModel.DataAnnotations.Schema;

namespace Cactus.Models.Database
{
    [Table("donator")]
    public class Donator
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        [Column("donation_target_id")]
        public int DonationTargetId { get; set; }
        [Column("donation_option_id")]
        public int DonationOptionId { get; set; }
        [Column("donation_target_type_id")]
        public int DonationTargetTypeId { get; set; }
        [Column("total_amount")]
        public decimal TotalAmount { get; set; }
        [Column("created")]
        public DateTime Created {  get; set; }
    }
}
