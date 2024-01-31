using Cactus.Models.Database;
using Cactus.Models.Responses;

namespace Cactus.Services.Interfaces
{
    public interface ISubscribeService
    {
        Task<BaseResponse<AuthorSubscribe>> SubscribeToAuthor(int subcriberId, int authorId);
        Task<BaseResponse<IEnumerable<AuthorSubscribe>>> GetPagingSubscribersAsync(int subcriberId, int authorPage, int pageSize);
        Task<BaseResponse<IEnumerable<AuthorSubscribe>>> GetSubscribersAsync(int subcriberId);
    }
}
