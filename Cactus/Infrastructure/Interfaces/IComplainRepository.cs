using Cactus.Models.Database;
using Cactus.Models.ViewModels;

namespace Cactus.Infrastructure.Interfaces
{
    public interface IComplainRepository:IBaseRepository<Complain>
    {
        Task<IEnumerable<Complain>> GetNotReviewedComplainsAsync(DateTime date);
        Task<bool> AddComplainAsync(Complain entity);
    }
}
