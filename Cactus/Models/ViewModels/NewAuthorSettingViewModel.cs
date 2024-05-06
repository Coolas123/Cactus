using System.ComponentModel.DataAnnotations;

namespace Cactus.Models.ViewModels
{
    public class NewAuthorSettingViewModel
    {
        [Display(Name = "Баннер")]
        public IFormFile? BannerFile { get; set; }

        [MaxLength(256, ErrorMessage = "Длина должна быть меньше 256-ти символов")]
        [Display(Name = "Описание профиля")]
        public string? Description { get; set; }
    }
}
