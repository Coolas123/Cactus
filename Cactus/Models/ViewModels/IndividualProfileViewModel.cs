using Cactus.Models.Database;

namespace Cactus.Models.ViewModels
{
    public class IndividualProfileViewModel
    {
        public string UrlPage { get; set; }
        public string AvatarPath { get; set; }
        public string BannerPath { get; set; }
        public User User { get; set; }
    }
}
