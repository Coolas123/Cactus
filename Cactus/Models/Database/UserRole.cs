using System.ComponentModel.DataAnnotations.Schema;

namespace Cactus.Models.Database
{
    public class UserRole
    {
        public int Id { get; set; }
        public string Name { get; set; }   
        public List<User> Users {  get; set; }
    }
}
