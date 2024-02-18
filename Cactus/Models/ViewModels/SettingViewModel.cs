using Cactus.Infrastructure;
using Cactus.Models.Database;
using SportsStore.Models;
using System.ComponentModel.DataAnnotations;

namespace Cactus.Models.ViewModels
{
    public class SettingViewModel
    {
        [Display(Name = "Аватарка")]
        public IFormFile AvatarFile {  get; set; }

        [Display(Name = "Баннер")]
        public IFormFile BannerFile { get; set; }

        [RegularExpression(@"[A-Za-z0-9]+", ErrorMessage = "Прозвище должно состоять из латинских букв")]
        [Display(Name = "Сменить прозвище")]
        public string UserName { get; set; }

        [EmailAddress(ErrorMessage = "Неверный формат почты")]
        [Display(Name = "Сменить почту")]
        public string Email { get; set; }


        [DataType(DataType.Date)]
        [DateTimeRange(minAge: 16, maxAge: 100)]
        [Display(Name = "Сменть дату рождения")]
        public DateTime DateOfBirth { get; set; }

        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Длина пароля должна быть больше 5-ти символов")]
        [Display(Name = "Сменить пароль")]
        public string Password { get; set; }


        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [Display(Name = "Повторите пароль")]
        public string ConfirmPassword { get; set; }

        public User User { get; set; }

        public string AvatarPath { get; set; }
        public string BannerPath { get; set; }
        public Author Author { get; set; }

        public RegisterAuthorViewModel RegisterAuthor { get; set; }


        [RegularExpression(@"[А-Яа-яЁёA-Za-z]+", ErrorMessage = "Имя должно состоять из букв")]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }


        [RegularExpression(@"[А-Яа-яЁёA-Za-z]+", ErrorMessage = "Фамилия должно состоять из букв")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }


        public PagingUninterestingAuthorsViewModel PagingUninterestingAuthors { get; set; }

    }
}
