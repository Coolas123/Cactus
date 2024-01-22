using Cactus.Models.Database;

namespace Cactus.Infrastructure.Interfaces
{
    public interface IMaterialRepository:IBaseRepository<Material>
    {
        Task<bool> UpdateAvatarAsync(Material entity);
        Task<Material> GetAvatarAsync(int userId);
        Task<Material> GetBannerAsync(int userId);
        Task<bool> UpdateBannerAsync(Material entity);
    }
}
