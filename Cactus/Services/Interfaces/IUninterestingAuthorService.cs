using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;

namespace Cactus.Services.Interfaces
{
    public interface IUninterestingAuthorService
    {
        Task<BaseResponse<UninterestingAuthor>> AddUninterestingAuthor(int userId, int authorId);
        Task<BaseResponse<IEnumerable<UninterestingAuthor>>> GetPagingUninterestingsAsync(int userId, int authorPage, int pageSize);
        Task<BaseResponse<IEnumerable<UninterestingAuthor>>> GetUninterestingsAsync(int userId);
        Task<BaseResponse<IEnumerable<UninterestingAuthor>>> GetUninterestingAuthorsViewAsync(int userId);
        Task<BaseResponse<UninterestingAuthor>> GetAuthorAsync(int userId, int authorId);
        Task<BaseResponse<bool>> RemoveUninterestingAuthor(int userId, int authorId);
    }
}
