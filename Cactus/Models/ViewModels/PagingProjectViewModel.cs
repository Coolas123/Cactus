using Cactus.Models.Database;
using SportsStore.Models;

namespace Cactus.Models.ViewModels
{
    public class PagingProjectViewModel
    {
        public IEnumerable<Project> Projects { get; set; }
        public PagingInfo ProjectsPagingInfo { get; set; }
    }
}
