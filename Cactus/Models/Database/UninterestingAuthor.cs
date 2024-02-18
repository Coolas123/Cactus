using System.ComponentModel.DataAnnotations.Schema;

namespace Cactus.Models.Database
{
    [Table("uninteresting_author")]
    public class UninterestingAuthor {
        [Column("id")]
        public int Id { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        public User User { get; set; }
        [Column("author_id")]
        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
