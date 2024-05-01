using System.ComponentModel.DataAnnotations.Schema;

namespace Cactus.Models.Database
{
    [Table("user_role")]
    public class UserRole
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name", TypeName = "varchar(64)")]
        public string Name { get; set; }   
        public List<User> Users {  get; set; }
    }
}
