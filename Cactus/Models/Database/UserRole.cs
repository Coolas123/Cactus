using System.ComponentModel.DataAnnotations.Schema;

namespace Cactus.Models.Database
{
    public class UserRole
    {
        public Guid Id { get; set; }
        public string Name { get; set; }   
    }
}
