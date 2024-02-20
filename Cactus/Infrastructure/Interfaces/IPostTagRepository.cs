using Cactus.Models.Database;

namespace Cactus.Infrastructure.Interfaces
{
    public interface IPostTagRepository:IBaseRepository<PostTag>
    {
        Task<bool> AddTagsToPost(IEnumerable<PostTag> tags);
    }
}
