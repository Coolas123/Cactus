using System.ComponentModel.DataAnnotations;
using Cactus.Infrastructure;
using Cactus.Models.Enums;

namespace Cactus.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [RegularExpression(@"[A-Za-z0-9]+", ErrorMessage = "Прозвище должно состоять из латинских букв")]
        [Display(Name = "Прозвище")]
        public string UserName { get; set; }


        [DataType(DataType.Date)]
        [DateTimeRange(minAge:16,maxAge:100)]
        [Display(Name = "Дата рождения")]
        public DateTime DateOfBirth { get; set; }


        [Display(Name = "Пол")]
        public string Gender { get; set; }


        [Display(Name = "Страна")]
        public Country? Country { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Длина пароля должна быть больше 5-ти символов")]
        [Display(Name = "Пароль")]
        public string Password { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [Display(Name = "Повторите пароль")]
        public string ConfirmPassword { get; set; }


        [Required]
        [EmailAddress(ErrorMessage = "Неверный формат почты")]
        [Display(Name = "Почта")]
        public string Email { get; set; }


        public string ReturnUrl { get; set; }
    }
}
