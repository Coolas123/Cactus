using Cactus.Models.Database;
using System.ComponentModel.DataAnnotations;

namespace Cactus.Models.ViewModels
{
    public class PostViewModel
    {
        [Display(Name = "Оглавление")]
        public string Title { get; set; }
        [Display(Name = "Описание")]
        public string Description { get; set; }
        public IFormFile PostPhoto { get; set; }
        public string PostPhotoPath { get; set; }
        [Display(Name = "Категории")]
        public int CategoryId {  get; set; }
    }
}
