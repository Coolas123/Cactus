using Cactus.Models.Database;

namespace Cactus.Models.ViewModels
{
    public class SettingViewModel
    {
        public NewSettingViewModel? NewSettingViewModel { get; set; }
        public User? User { get; set; }
        public string? AvatarPath { get; set; }
        public string? BannerPath { get; set; }
        public Author? Author { get; set; }
        public RegisterAuthorViewModel? RegisterAuthor { get; set; }
    }
}
