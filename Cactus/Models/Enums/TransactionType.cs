using System.ComponentModel.DataAnnotations;

namespace Cactus.Models.Enums
{
    public enum TransactionType
    {
        [Display(Name ="Пополнение")]
        Replenish=1,
        [Display(Name = "Вывод")]
        Withdraw =2,
        [Display(Name = "Системная покупка")]
        IntrasystemOperations =3
    }
}
