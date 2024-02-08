using System.ComponentModel.DataAnnotations.Schema;

namespace Cactus.Models.Database
{
    public class Project
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string UrlProject { get; set; }
        public ProjectStatus StatusId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int OrganizationId { get; set; }
        public decimal Pledged { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Goal { get; set; }
    }
}
