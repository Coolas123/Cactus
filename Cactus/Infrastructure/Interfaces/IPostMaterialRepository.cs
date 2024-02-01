using Cactus.Models.Database;

namespace Cactus.Infrastructure.Interfaces
{
    public interface IPostMaterialRepository:IBaseRepository<PostMaterial>
    {
        Task<PostMaterial> GetPhotoAsync(int postId);
    }
}
