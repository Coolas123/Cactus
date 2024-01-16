using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;

namespace Cactus.Services.Interfaces
{
    public interface IMaterialService
    {
        Task<BaseResponse<bool>> ChangeAvatarAsync(IFormFile file, string email);
        Task<BaseResponse<Material>> GetAvatarAsync(string email);
        Task<BaseResponse<Material>> GetAvatarAsync(int id);
    }
}
