using System.ComponentModel.DataAnnotations;
using Cactus.Infrastructure;
using Cactus.Models.Enums;

namespace Cactus.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Введите прозвище")]
        [RegularExpression(@"[A-Za-z0-9]+", ErrorMessage = "Прозвище должно состоять из латинских букв")]
        [Display(Name = "Прозвище")]
        public string UserName { get; set; }


        [DataType(DataType.Date)]
        [DateTimeRange(minAge:16,maxAge:100)]
        [Display(Name = "Дата рождения")]
        public DateTime DateOfBirth { get; set; }


        [Display(Name = "Пол")]
        [Required(ErrorMessage = "Выберите пол")]
        public string Gender { get; set; }


        [Display(Name = "Страна")]
        [Required(ErrorMessage = "Выберите страну")]
        public Country? Country { get; set; }


        [Required(ErrorMessage ="Требуется пароль")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Длина пароля должна быть больше 5-ти символов")]
        [MaxLength(20, ErrorMessage = "Длина пароля должна быть меньше 20-ти символов")]
        [RegularExpression(@"((?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%!]).{6,20})", ErrorMessage = "Пароль должен содержать: Латинскую букву в нижнем и верхнем регистре, одна цифра, спецсимвол @#$%!")]
        [Display(Name = "Пароль")]
        public string Password { get; set; }


        [Required(ErrorMessage = "Повторите пароль")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [Display(Name = "Повторите пароль")]
        public string ConfirmPassword { get; set; }


        [Required(ErrorMessage = "Требуется почта")]
        [EmailAddress(ErrorMessage = "Неверный формат почты")]
        [Display(Name = "Почта")]
        public string Email { get; set; }


        public string ReturnUrl { get; set; }
    }
}
