using Cactus.Models.Database;
using System.ComponentModel.DataAnnotations;

namespace Cactus.Models.Enums
{
    public enum ContentType
    {
        [Display(Name ="Пост")]
        Post=1,
        [Display(Name = "Пользователь")]
        User =2,
        [Display(Name = "Комментарий")]
        Comment =3
    }
}
