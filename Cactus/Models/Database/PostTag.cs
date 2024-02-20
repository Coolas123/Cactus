using System.ComponentModel.DataAnnotations.Schema;

namespace Cactus.Models.Database
{
    [Table("post_tag")]
    public class PostTag {
        [Column("post_id")]
        public int PostId { get; set; }
        [Column("tag_id")]
        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
