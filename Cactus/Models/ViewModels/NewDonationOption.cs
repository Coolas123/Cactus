using System.ComponentModel.DataAnnotations;

namespace Cactus.Models.ViewModels
{
    public class NewDonationOption
    {
        public int AuthorId { get; set; }
        [Display(Name = "Введите описание")]
        public string Description { get; set; }
        [Display(Name = "Название опции")]
        public string OptionName { get; set; }
        [Display(Name = "Миниальная цена")]
        public decimal MinPrice { get; set; }
        [Display(Name = "Максимальная цена")]
        public decimal MaxPrice { get; set; }
        [Display(Name = "Тип монетизации")]
        public int MonetizationTypeId { get; set; }
    }
}
