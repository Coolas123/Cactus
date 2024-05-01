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
        [Column("url_page", TypeName = "varchar(64)")]
        public string UrlPage {  get; set; }
        [Column("firstname", TypeName = "varchar(64)")]
        public string? Firstname { get; set; }
        [Column("lastname", TypeName = "varchar(64)")]
        public string? Lastname { get; set; }
        [Column("surname", TypeName = "varchar(64)")]
        public string? Surname { get; set; }
    }
}
