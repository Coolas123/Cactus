using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cactus.Models.Database
{
    public class SystemRole : IdentityRole<Guid>
    {
    }
}
