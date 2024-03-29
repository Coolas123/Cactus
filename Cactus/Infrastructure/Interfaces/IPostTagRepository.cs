using Cactus.Models.Database;

namespace Cactus.Infrastructure.Interfaces
{
    public interface IPostTagRepository:IBaseRepository<PostTag>
    {
        Task<bool> AddTagsToPostAsync(IEnumerable<PostTag> tags);
        Task<IEnumerable<Tag>> GetPostTagsAsync(int postId);
        Task<IEnumerable<Post>> GetPostsByTagsAsync(IEnumerable<Tag> tags);
        Task<IEnumerable<Author>> GetAuthorsByTagsAsync(IEnumerable<Tag> tags);

    }
}
