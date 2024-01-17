using System.ComponentModel.DataAnnotations;

namespace Cactus.Models.ViewModels
{
    public class RegisterIndividualViewModel
    {
        [Required]
        [Display(Name ="Адрес страницы")]
        [RegularExpression(@"[A-Za-z]+", ErrorMessage = "Адрес страницы должен состоять из латинских букв")]
        public string UrlPage {  get; set; }
        public string ReturnUrl { get; set; }
    }
}
