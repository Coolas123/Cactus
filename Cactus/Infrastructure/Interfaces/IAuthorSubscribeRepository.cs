using Cactus.Models.Database;

namespace Cactus.Infrastructure.Interfaces
{
    public interface IAuthorSubscribeRepository:IBaseRepository<AuthorSubscribe>
    {
        Task<AuthorSubscribe> GetSubscriptionAsync(int subcriberId, int authorId);
        Task<IEnumerable<AuthorSubscribe>> GetPagingSubscribersAsync(int subcriberId, int authorPage, int pageSize);
        Task<IEnumerable<AuthorSubscribe>> GetSubscribersAsync(int subcriberId);
        Task<AuthorSubscribe> GetSubscribe(int userId, int authorId);
    }
}
