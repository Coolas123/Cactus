using Cactus.Models.Database;
using Cactus.Models.ViewModels;

namespace Cactus.Infrastructure.Interfaces
{
    public interface IComplainRepository:IBaseRepository<Complain>
    {
        Task<IEnumerable<ComplainViewModel>> GetNotReviewedComplains(DateTime date);
        Task<bool> AddComplain(Complain entity);
    }
}
