using System.ComponentModel.DataAnnotations.Schema;

namespace Cactus.Models.Database
{
    [Table("post_one_time_purschase_donator")]
    public class PostOneTimePurschaseDonator
    {
        [Column("post_Id")]
        public int PostId { get; set; }
        [Column("donator_Id")]
        public int DonatorId { get; set; }
        public Donator Donator { get; set; }
    }
}
