using System.ComponentModel.DataAnnotations;

namespace Cactus.Models.ViewModels
{
    public class NewComplainViewModel
    {
        public int SenderId { get; set; }
        public int? UserId { get; set; }
        public int? PostId { get; set; }
        public int? CommentId { get; set; }
        [Display(Name = "Опишите причину")]
        public string? Description { get; set; }
        public DateTime Created { get; set; }
        [Display(Name = "Выберите тип жалобы")]
        public int ComplainTypeId { get; set; }
        public string ReturnUrl { get; set; }
    }
}
