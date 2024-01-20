using Cactus.Models.Database;

namespace Cactus.Infrastructure.Interfaces
{
    public interface IIndividualRepository:IBaseRepository<Individual>
    {
        Task<Individual> getByUrlPage(string urlPage);
    }
}
