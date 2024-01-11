using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Cactus.Models.Database
{
    public class ProjectSubscribe
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [ForeignKey("Project")]
        public int SubscriptionId { get; set; }
        public Project Subscription { get; set; }
    }
}
