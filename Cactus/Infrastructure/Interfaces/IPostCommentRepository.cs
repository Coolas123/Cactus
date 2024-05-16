using Cactus.Models.Database;

namespace Cactus.Infrastructure.Interfaces
{
    public interface IPostCommentRepository : IBaseRepository<PostComment>
    {
        Task<IEnumerable<PostComment>> GetCommentsAsync(int postId);
        Task<PostComment> GetCommentAsync(int postId);
    }
}
