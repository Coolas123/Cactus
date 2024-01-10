using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cactus.Models.Database
{
    public class Patron
    {
        [Key]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
