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
        [Column("user_id")]
        public int? UserId { get; set; }
        [Column("post_id")]
        public int? PostId { get; set; }
        [Column("comment_id")]
        public int? CommentId { get; set; }
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
    }
}
