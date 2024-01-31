using Cactus.Models.Database;

namespace Cactus.Infrastructure.Interfaces
{
    public interface IIndividualRepository:IBaseRepository<Individual>
    {
        Task<Individual> GetUserByUrlPageAsync(string urlPage);
        Task<Individual> GetByUrlPageAsync(string urlPage);
    }
}
