using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;

namespace Cactus.Services.Interfaces
{
    public interface IPostService
    {
        Task<BaseResponse<Post>> AddPost(PostViewModel model, int id);
    }
}
