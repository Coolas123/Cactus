using System.ComponentModel.DataAnnotations;

namespace Cactus.Models.ViewModels
{
    public class NewComplainViewModel
    {
        public int SenderId { get; set; }
        [Display(Name = "Опишите причину")]
        [Required(ErrorMessage = "Опишите причину жалобы")]
        [MaxLength(512,ErrorMessage ="Сообщение не должно превышать 512 символов")]
        public string Description { get; set; }
        public DateTime Created { get; set; }
        [Display(Name = "Выберите тип жалобы")]
        public int ComplainTypeId { get; set; }
        public int ComplainTargetTypeId { get; set; }
        public int ComplainTargetId { get; set; }
        public string ReturnUrl { get; set; }
    }
}
