using Cactus.Models.Database;
using SportsStore.Models;

namespace Cactus.Models.ViewModels
{
    public class PagingIndividualViewModel
    {
        public IEnumerable<AuthorSubscribe> Authors { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public User CurrentUser { get; set; }
    }
}
