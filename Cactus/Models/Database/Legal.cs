using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cactus.Models.Database
{
    public class Legal
    {
        [Key]
        [ForeignKey("User")]
        public Guid UserId { get; set; }
    }
}
