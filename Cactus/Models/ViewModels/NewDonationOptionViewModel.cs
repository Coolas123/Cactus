using System.ComponentModel.DataAnnotations;

namespace Cactus.Models.ViewModels
{
    public class NewDonationOptionViewModel
    {
        [Display(Name = "Обложка")]
        public IFormFile? CoverFile { get; set; }
        public int AuthorId { get; set; }
        [Display(Name = "Описание")]
        public string Description { get; set; }
        [Display(Name = "Название")]
        public string OptionName { get; set; }
        [Display(Name = "Цена")]
        public decimal Price { get; set; }
        public int MonetizationTypeId { get; set; }
    }
}
