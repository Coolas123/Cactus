using Cactus.Models.Database;
using Cactus.Models.ViewModels;

namespace Cactus.Infrastructure.Interfaces
{
    public interface IComplainRepository:IBaseRepository<Complain>
    {
        Task<IEnumerable<ComplainViewModel>> GetNotReviewedComplains(int minCountComplains, DateTime date);
    }
}
