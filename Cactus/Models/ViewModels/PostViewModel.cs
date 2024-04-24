using Cactus.Models.Database;
using System.ComponentModel.DataAnnotations;

namespace Cactus.Models.ViewModels
{
    public class PostViewModel
    {
        [Required(ErrorMessage ="Введите оглавление")]
        [Display(Name = "Оглавление")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Введите описание")]
        [Display(Name = "Описание")]
        public string Description { get; set; }
        public IFormFile? PostPhoto { get; set; }
        public string? PostPhotoPath { get; set; }
        [Required(ErrorMessage = "Выберите категорию")]
        [Display(Name = "Категории")]
        public int CategoryId {  get; set; }
        public DateTime? Created {  get; set; }
        [Display(Name = "Теги поста")]
        [RegularExpression(@"#\\?[A-Za-zа-яА-Я]+", ErrorMessage = "Тег должен состоять из букв без спец символов в конце")]
        public string? Tags { get; set; }
        [Display(Name = "Сделать пост бесплатным?")]
        public bool IsFree { get; set; }
    }
}
