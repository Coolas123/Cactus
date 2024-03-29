using Cactus.Models.Database;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Cactus.Infrastructure.Interfaces
{
    public interface IDonatorRepository:IBaseRepository<Donator>
    {
        Task<Donator> GetDonatorAsync(int targetId, int typeId, int userId);
        Task<IEnumerable<Donator>> GetDonatorsAsync(int userId);
        Task<Dictionary<int,decimal>> GetCollectedSumOfGoalsAsync(List<int> optionId);
    }
}
