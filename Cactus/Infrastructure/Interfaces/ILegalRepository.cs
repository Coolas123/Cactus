using Cactus.Models.Database;

namespace Cactus.Infrastructure.Interfaces
{
    public interface ILegalRepository:IBaseRepository<Legal>
    {
        Task<Legal> GetByUrlPageAsync(string urlPage);
        Task<Legal> GetUserByUrlPageAsync(string urlPage);
    }
}
