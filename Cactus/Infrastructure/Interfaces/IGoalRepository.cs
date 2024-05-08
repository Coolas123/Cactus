using Cactus.Models.Database;

namespace Cactus.Infrastructure.Interfaces
{
    public interface IGoalRepository:IBaseRepository<Goal>
    {
        Task<bool> UpdateAsync(Goal entiry);
        Task<IEnumerable<Goal>> GetWorkGoalsAsync(int authorId);
    }
}
