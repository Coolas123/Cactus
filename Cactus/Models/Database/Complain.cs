using System.ComponentModel.DataAnnotations.Schema;

namespace Cactus.Models.Database
{
    [Table("complain")]
    public class Complain
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("sender_id")]
        public int SenderId { get; set; }
        [Column("description")]
        public string? Description { get; set; }
        [Column("created")]
        public DateTime Created { get; set; }
        [Column("admin_id")]
        public int? AdminId { get; set; }
        [Column("complain_status_id")]
        public int ComplainStatusId { get; set; }
        [Column("complain_type_id")]
        public int ComplainTypeId { get; set; }
        [Column("complain_target_type_id")]
        public int ComplainTargetTypeId { get; set; }
        [Column("complain_target_id")]
        public int ComplainTargetId { get; set; }
    }
}
