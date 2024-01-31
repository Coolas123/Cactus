using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;

namespace Cactus.Services.Interfaces
{
    public interface IMaterialService
    {
        Task<BaseResponse<string>> ChangeAvatarAsync(IFormFile file, int id);
        Task<BaseResponse<Material>> GetAvatarAsync(string email);
        Task<BaseResponse<Material>> GetAvatarAsync(int id);
        Task<BaseResponse<Material>> GetBannerAsync(string email);
        Task<BaseResponse<Material>> GetBannerAsync(int id);
        Task<BaseResponse<string>> ChangeBannerAsync(IFormFile file, int id);
        Task<BaseResponse<IndividualProfileViewModel>> GetProfileMaterials(int id);
    }
}
