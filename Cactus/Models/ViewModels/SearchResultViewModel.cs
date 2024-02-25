using Cactus.Models.Database;

namespace Cactus.Models.ViewModels
{
    public class SearchResultViewModel
    {
        public IEnumerable<Post>? Posts { get; set; }
        public string? PostDescription { get; set; }
        public IEnumerable<Author>? Authors { get; set; }
        public string? AuthorDescription { get; set; }
    }
}
