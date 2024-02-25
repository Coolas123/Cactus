using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;

namespace Cactus.Services.Interfaces
{
    public interface INewsFeedService
    {
        Task<BaseResponse<SearchResultViewModel>> Search(SearchViewModel model);
    }
}
