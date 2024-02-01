using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;

namespace Cactus.Services.Interfaces
{
    public interface IPostMaterialService
    {
        Task<BaseResponse<PostMaterial>> GetPhotoAsync(int id);
        Task<BaseResponse<PostMaterial>> AddPhotoAsync(IFormFile file, int id);
    }
}
