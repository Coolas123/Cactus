using System.ComponentModel.DataAnnotations;

namespace Cactus.Models.Enums
{
    public enum ComplainType
    {
        [Display(Name = "Спам")]
        Spam =1,
        [Display(Name = "Обман")]
        Deception =2
    }
}
