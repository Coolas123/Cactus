using System.ComponentModel.DataAnnotations;

namespace Cactus.Models.ViewModels
{
    public class RegisterAuthorViewModel
    {
        [Required(ErrorMessage="Поле адреса страницы не должно быть пустым")]
        [Display(Name ="Адрес страницы")]
        [RegularExpression(@"[A-Za-z]+", ErrorMessage = "Адрес страницы должен состоять из латинских букв")]
        public string UrlPage {  get; set; }
    }
}
