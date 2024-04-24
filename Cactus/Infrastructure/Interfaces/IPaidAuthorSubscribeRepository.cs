using Cactus.Models.Database;

namespace Cactus.Infrastructure.Interfaces
{
    public interface IPaidAuthorSubscribeRepository:IBaseRepository<PaidAuthorSubscribe>
    {
        Task<bool> SubscribeToAuthorAsync(PaidAuthorSubscribe entity);
        Task<IEnumerable<PaidAuthorSubscribe>> GetCurrentSubscribes(int donatorId);
    }
}
