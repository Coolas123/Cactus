using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;

namespace Cactus.Services.Interfaces
{
    public interface IPostService
    {
        Task<BaseResponse<Post>> AddPost(PostViewModel model, int id);
        Task<BaseResponse<IEnumerable<Post>>> GetPagingPostsAsync(int authorId, int postPage, int pageSize);
        Task<BaseResponse<IEnumerable<Post>>> GetPostsAsync(int authorId);
        Task<BaseResponse<Post>> GetPostByIdAsync(int postId);
        Task<BaseResponse<PagingAuthorViewModel>> GetUserViewPostsAsync(int userId, int postPage, int PageSize);
        Task<BaseResponse<Post>> GetLastAsync(DateTime created);
        Task<BaseResponse<IEnumerable<Post>>> GetPostsByTagsAsync(IEnumerable<string> tags);
        Task<BaseResponse<IEnumerable<Post>>> GetPostsByTitleAsync(IEnumerable<string> titles);
    }
}
