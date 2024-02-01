using Cactus.Models.Database;
using Cactus.Models.ViewModels;

namespace Cactus.Infrastructure.Interfaces
{
    public interface IProfileMaterialRepository:IBaseRepository<ProfileMaterial>
    {
        Task<bool> UpdateAvatarAsync(ProfileMaterial entity);
        Task<ProfileMaterial> GetAvatarAsync(int userId);
        Task<ProfileMaterial> GetBannerAsync(int userId);
        Task<bool> UpdateBannerAsync(ProfileMaterial entity);
        Task<IEnumerable<ProfileMaterial>> GetProfileMaterialAsync(int userId);
    }
}
