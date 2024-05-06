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
        [Column("description", TypeName = "varchar(256)")]
        public string? Description { get; set; }
    }
}
