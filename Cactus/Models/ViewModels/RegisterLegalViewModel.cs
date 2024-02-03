using System.ComponentModel.DataAnnotations;

namespace Cactus.Models.ViewModels
{
    public class RegisterLegalViewModel
    {
        [Required]
        [Display(Name = "Адрес страницы")]
        [RegularExpression(@"[A-Za-z]+", ErrorMessage = "Адрес страницы должен состоять из латинских букв")]
        public string UrlPage { get; set; }
    }
}
