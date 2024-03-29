using Cactus.Models.Database;

namespace Cactus.Infrastructure.Interfaces
{
    public interface IPaidAuthorSubscribeRepository:IBaseRepository<PaidAuthorSubscribe>
    {
        Task<bool> SubscribeToAuthor(PaidAuthorSubscribe entity);
    }
}
