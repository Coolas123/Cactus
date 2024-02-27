using System.ComponentModel.DataAnnotations;

namespace Cactus.Models.Enums
{
    public enum ComplainStatus
    {
        [Display(Name ="Не рассмотрено")]
        NotReviewed=1,
        [Display(Name = "Рассмотрено")]
        Reviewed =2
    }
}
