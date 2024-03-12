using Cactus.Models.Database;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Cactus.Infrastructure.Interfaces
{
    public interface IDonatorRepository:IBaseRepository<Donator>
    {
        Task<Donator> GetDonator(int targetId, int typeId, int userId);
        Task<Dictionary<int,decimal>> GetCollectedSumOfGoals(List<int> optionId);
    }
}
