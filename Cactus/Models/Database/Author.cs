using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Cactus.Models.Database
{
    [Table("author")]
    public class Author
    {
        [Key]
        [ForeignKey("User")]
        [Column("user_id")]
        public int UserId { get; set; }
        public User User { get; set; }
        [Column("url_page")]
        public string UrlPage {  get; set; }
        [Column("firstname")]
        public string? Firstname { get; set; }
        [Column("lastname")]
        public string? Lastname { get; set; }
        [Column("surname")]
        public string? Surname { get; set; }
    }
}
