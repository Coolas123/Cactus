using Cactus.Models.Database;

namespace Cactus.Infrastructure.Interfaces
{
    public interface IProjectSubscribeRepository:IBaseRepository<ProjectSubscribe>
    {
        Task<IEnumerable<ProjectSubscribe>> GetPagingSubscribersAsync(int subcriberId, int projectPage, int pageSize);
        Task<IEnumerable<ProjectSubscribe>> GetSubscribersAsync(int subcriberId);
        Task<ProjectSubscribe> GetSubscriptionAsync(int subcriberId, int projectId);
    }
}
