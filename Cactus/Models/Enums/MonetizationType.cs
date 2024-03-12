using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Cactus.Models.Enums
{
    public enum MonetizationType
    {
        [Display(Name = "Разовая покупка")]
        OneTimePurchase =1,
        [Display(Name = "Уровень подписки")]
        SubLevel =2,
        [Display(Name = "Цель")]
        Goal =3
    }
}
