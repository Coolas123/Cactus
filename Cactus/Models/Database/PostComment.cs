using System.ComponentModel.DataAnnotations.Schema;

namespace Cactus.Models.Database
{
    [Table("post_comment")]
    public class PostComment
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        public User User { get; set; }
        [Column("post_id")]
        public int PostId { get; set; }
        [Column("comment")]
        public string Comment { get; set; }
        [Column("created")]
        public DateTime Created {  get; set; }
    }
}
