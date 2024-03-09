using System.ComponentModel.DataAnnotations;

namespace Cactus.Models.ViewModels
{
    public class NewSubLevelDonationOptionViewModel
    {
        [Display(Name = "Обложка")]
        public IFormFile? CoverFile { get; set; }
        public NewDonationOption NewDonationOption { get; set; }
    }
}
