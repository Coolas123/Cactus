using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cactus.Models.Database
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string HashPassword { get; set; }
        public int SystemRoleId { get; set; }
        public SystemRole SystemRole { get; set; }
        public int UserRoleId { get; set; }
        public UserRole UserRole { get; set; }
        public int? CountryId { get; set; }
        public Country Country { get; set; }
        public byte WarnAmount { get; set; }
        public bool IsBan { get; set; }
    }
}
