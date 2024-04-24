using System.ComponentModel.DataAnnotations;

namespace Cactus.Models.ViewModels
{
    public class NewDonationOptionViewModel
    {
        [Display(Name = "Обложка")]
        public IFormFile? CoverFile { get; set; }
        [Required]
        public int AuthorId { get; set; }
        [Required(ErrorMessage = "Введите описание опции")]
        [Display(Name = "Описание")]
        public string Description { get; set; }
        [Required(ErrorMessage ="Введите название опции")]
        [Display(Name = "Название")]
        public string OptionName { get; set; }
        [Display(Name = "Цена")]
        [Required(ErrorMessage = "Введите цену опции")]
        [Range(1,int.MaxValue,ErrorMessage ="Неверная цена")]
        public decimal Price { get; set; }
        [Required]
        public int MonetizationTypeId { get; set; }
    }
}
