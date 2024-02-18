using Cactus.Models.Database;
using SportsStore.Models;

namespace Cactus.Models.ViewModels
{
    public class PagingUninterestingAuthorsViewModel
    {
        public PagingInfo PagingInfo { get; set; }
        public IEnumerable<UninterestingAuthor> UninterestingAuthors { get; set; }
    }
}
