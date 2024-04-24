using Cactus.Models.Database;

namespace Cactus.Models.ViewModels
{
    public class NewPostViewModel
    {
        public PostViewModel Post { get; set; }
        public int? SelectedDonationOption { get; set; }
        public NewDonationOptionViewModel? NewDonationOption { get; set; }
        public IEnumerable<DonationOption>? DonationOptions { get; set; }
        public IEnumerable<Category>? Categories { get; set; }
    }
}
