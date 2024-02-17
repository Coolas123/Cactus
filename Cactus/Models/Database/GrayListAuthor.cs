using System.ComponentModel.DataAnnotations.Schema;

namespace Cactus.Models.Database
{
    [Table("gray_list_author")]
    public class GrayListAuthor {
        [Column("id")]
        public int Id { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        [Column("author_id")]
        public int AuthorId { get; set; }
    }
}
