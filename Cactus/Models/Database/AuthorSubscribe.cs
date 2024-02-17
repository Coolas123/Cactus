using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cactus.Models.Database
{
    [Table("author_subscribe")]
    public class AuthorSubscribe
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        [ForeignKey("User")]
        [Column("author_id")]
        public int AuthorId {  get; set; }
        public User Author { get; set; }
    }
}
