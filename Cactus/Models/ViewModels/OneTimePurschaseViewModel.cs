namespace Cactus.Models.ViewModels
{
    public class OneTimePurschaseViewModel
    {
        public decimal Sended { get; set; }
        public DateTime Created { get; set; }
        public int PayMethodId { get; set; }
        public decimal Received { get; set; }
        public int StatusId { get; set; }
        public int UserId { get; set; }
        public int AuthorId { get; set; }
        public int DonationOptionId { get; set; }
    }
}
