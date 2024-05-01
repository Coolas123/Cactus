using System.ComponentModel.DataAnnotations.Schema;

namespace Cactus.Models.Database
{
    [Table("user")]
    public class User
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("user_name", TypeName = "varchar(64)")]
        public string UserName { get; set; }
        [Column(name:"date_of_birth", TypeName = "Date")]
        public DateTime DateOfBirth { get; set; }
        [Column("email", TypeName = "varchar(64)")]
        public string Email { get; set; }
        [Column("gender", TypeName = "varchar(32)")]
        public string Gender { get; set; }
        [Column("hash_password")]
        public string HashPassword { get; set; }
        [Column("system_role_id")]
        public int SystemRoleId { get; set; }
        public SystemRole SystemRole { get; set; }
        [Column("user_role_id")]
        public int UserRoleId { get; set; }
        public UserRole UserRole { get; set; }
        [Column("country_id")]
        public int? CountryId { get; set; }
        public Country Country { get; set; }
        [Column("warn_amount")]
        public byte WarnAmount { get; set; }
        [Column("is_ban")]
        public bool IsBan { get; set; }
    }
}
