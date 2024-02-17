using System.ComponentModel.DataAnnotations.Schema;

namespace Cactus.Models.Database
{
    [Table("post_category")]
    public class PostCategory {
        [Column("post_id")]
        public int PostId {  get; set; }
        [Column("category_id")]
        public int CategoryId { get; set; }
    }
}
