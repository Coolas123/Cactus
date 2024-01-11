using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Cactus.Models.Database
{
    public class ProjectSubscribe
    {
        [Key]
        [ForeignKey("User")]
        public int UserId { get; set; }
        [ForeignKey("Project")]
        public int SubscriptionId { get; set; }
        public Project Subscription { get; set; }
    }
}
