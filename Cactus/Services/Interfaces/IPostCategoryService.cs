using Cactus.Models.Responses;

namespace Cactus.Services.Interfaces
{
    public interface IPostCategoryService
    {
        Task<BaseResponse<bool>> CreateAsync(int postId, int categoryId);
    }
}
