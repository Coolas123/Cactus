using System.ComponentModel.DataAnnotations;

namespace Cactus.Models.Enums
{
    public enum ComplainTargetType
    {
        [Display(Name = "Пост")]
        Post = 1,
        [Display(Name = "Пользователь")]
        User = 2,
        [Display(Name = "Комментарий")]
        Comment = 3
    }
}
