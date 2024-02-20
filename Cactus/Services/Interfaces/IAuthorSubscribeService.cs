using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;

namespace Cactus.Services.Interfaces
{
    public interface IAuthorSubscribeService
    {
        Task<BaseResponse<AuthorSubscribe>> SubscribeToAuthor(int subcriberId, int authorId);
        Task<BaseResponse<IEnumerable<AuthorSubscribe>>> GetPagingSubscribersAsync(int subcriberId, int authorPage, int pageSize);
        Task<BaseResponse<IEnumerable<AuthorSubscribe>>> GetSubscribersAsync(int subcriberId);
        Task<BaseResponse<PagingAuthorViewModel>> GetUserViewSubscribersAsync(int userId, int authorPage, int PageSize);
        Task<BaseResponse<AuthorSubscribe>> GetSubscribe(int userId, int authorId);
    }
}
