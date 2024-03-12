using System.ComponentModel.DataAnnotations.Schema;

namespace Cactus.Models.Database
{
    [Table("paid_author_subscribe")]
    public class PaidAuthorSubscribe {
        [Column("id")]
        public int Id { get; set; }
        [Column("donator_id")]
        public int DonatorId { get; set; }
        public Donator Donator { get; set; }
        [Column("start_date")]
        public DateTime StartDate { get; set; }
        [Column("end_date")]
        public DateTime EndDate { get; set; }
    }
}
