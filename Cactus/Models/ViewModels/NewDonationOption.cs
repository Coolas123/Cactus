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
        [Display(Name = "Цена")]
        public decimal Price { get; set; }
        [Display(Name = "Максимальная цена")]
        public int MonetizationTypeId { get; set; }
    }
}
