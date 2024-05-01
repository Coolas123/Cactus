using System.ComponentModel.DataAnnotations;

namespace Cactus.Models.ViewModels
{
    public class CommentViewModel
    {
        [MaxLength(512,ErrorMessage ="дли не может превышать 256 символов")]
        [Required(ErrorMessage ="Введите содержание комментария")]
        public string Comment {  get; set; }
        [Required]
        public int PostId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public DateTime Created { get; set; }
    }
}
