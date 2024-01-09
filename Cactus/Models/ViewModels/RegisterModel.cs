using System.ComponentModel.DataAnnotations;
using Cactus.Infrastructure;

namespace Cactus.Models.ViewModels
{
    public class RegisterModel
    {
        [Required]
        [RegularExpression(@"[А-Яа-яЁёA-Za-z]+", ErrorMessage = "Имя должно состоять из букв")]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }


        [Required]
        [RegularExpression(@"[А-Яа-яЁёA-Za-z]+", ErrorMessage = "Фамилия должно состоять из букв")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }


        [RegularExpression(@"[А-Яа-яЁёA-Za-z]+", ErrorMessage = "Отчество должно состоять из букв")]
        [Display(Name = "Отчество")]
        public string? Surname { get; set; }


        [DataType(DataType.Date)]
        [DateTimeRange(minAge:16,maxAge:100)]
        [Display(Name = "Дата рождения")]
        public DateTime DateOfBirth { get; set; }


        [Required]
        [Display(Name = "Пол")]
        public string Gender { get; set; }


        [Required]
        [MinLength(6, ErrorMessage = "Длина пароля должна быть больше 5-ти символов")]
        [Display(Name = "Пароль")]
        public string Password { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [Display(Name = "Повторите пароль")]
        public string ConfirmPassword { get; set; }


        [EmailAddress(ErrorMessage = "Неверный формат почты")]
        [Display(Name = "Почта")]
        public string? Email { get; set; }


        public string ReturnUrl { get; set; }
    }

    public class RegisterGoogleModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Неверный формат почты")]
        [Display(Name = "Почта")]
        public string? Email { get; set; }


        [Required]
        [DataType(DataType.Password, ErrorMessage = "Пароль должен содержать буквы и цифры")]
        [MinLength(6, ErrorMessage = "Длина пароля должна быть больше 5-ти символов")]
        [Display(Name = "Пароль")]
        public string Password { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [Display(Name = "Повторите пароль")]
        public string ConfirmPassword { get; set; }


        public string ReturnUrl { get; set; }
    }
}
