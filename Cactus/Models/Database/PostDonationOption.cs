using System.ComponentModel.DataAnnotations.Schema;

namespace Cactus.Models.Database
{
    [Table("post_donation_option")]
    public class PostDonationOption
    {
        [Column("post_id")]
        public int PostId { get; set; }
        [Column("donation_option_id")]
        public int DonationOptionId { get; set; }
        public DonationOption DonationOption { get; set; }
    }
}
