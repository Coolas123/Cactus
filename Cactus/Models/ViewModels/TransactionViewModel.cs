using System.ComponentModel.DataAnnotations;

namespace Cactus.Models.ViewModels
{
    public class TransactionViewModel
    {
        public int UserId { get; set; }
        [Display(Name ="Выберите метод пополнения")]
        [Required]
        public int PayMethodId { get; set; }
        public decimal Comission { get; set; }
        [Display(Name = "Выберите сумму пополнения")]
        public decimal Sended {  get; set; }
        [Display(Name = "Сумма после комиссии")]
        public decimal Received {  get; set; }
        public DateTime Created { get; set; }
        public int StatusId { get; set; }
        public int AuthorId { get; set; }
        public int PostId { get; set; }
        public int DonationOptionId { get; set; }
    }
}
