using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Cactus.Models.Database
{
    public class User:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Surname { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
    }
}
