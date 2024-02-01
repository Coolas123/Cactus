using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;

namespace Cactus.Services.Interfaces
{
    public interface IProfileMaterialService
    {
        Task<BaseResponse<string>> ChangeAvatarAsync(IFormFile file, int id);
        Task<BaseResponse<ProfileMaterial>> GetAvatarAsync(string email);
        Task<BaseResponse<ProfileMaterial>> GetAvatarAsync(int id);
        Task<BaseResponse<ProfileMaterial>> GetBannerAsync(string email);
        Task<BaseResponse<ProfileMaterial>> GetBannerAsync(int id);
        Task<BaseResponse<string>> ChangeBannerAsync(IFormFile file, int id);
        Task<BaseResponse<IndividualProfileViewModel>> GetProfileMaterials(int id);
    }
}
