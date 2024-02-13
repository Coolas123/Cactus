using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Cactus.Models.Database
{
    public class Author
    {
        [Key]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
        public string UrlPage {  get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Surname { get; set; }
    }
}
