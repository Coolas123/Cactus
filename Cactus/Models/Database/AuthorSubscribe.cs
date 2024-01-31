using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cactus.Models.Database
{
    public class AuthorSubscribe
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [ForeignKey("User")]
        public int AuthorId {  get; set; }
        public User Author { get; set; }
    }
}
