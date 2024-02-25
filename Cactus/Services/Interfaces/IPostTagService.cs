using Cactus.Models.Database;
using Cactus.Models.Responses;

namespace Cactus.Services.Interfaces
{
    public interface IPostTagService
    {
        Task<BaseResponse<bool>> AddTagsToPost(int postId, List<string> tags);
        Task<BaseResponse<IEnumerable<Tag>>> GetPostTagsAsync(int postId);
        Task<BaseResponse<IEnumerable<Post>>> GetPostsByTagsAsync(IEnumerable<Tag> tags);
        Task<BaseResponse<IEnumerable<Author>>> GetAuthorsByTagsAsync(IEnumerable<string> tags);
    }
}
