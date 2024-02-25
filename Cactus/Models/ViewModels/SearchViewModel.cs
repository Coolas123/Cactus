using Cactus.Models.Database;
using Nest;

namespace Cactus.Models.ViewModels
{
    public class SearchViewModel
    {
        public string SearchText { get; set; }
        public bool IsAuthor { get; set; }
        public bool IsPost { get; set; }
        public SearchResultViewModel searchResponse { get; set; }
    }
}
