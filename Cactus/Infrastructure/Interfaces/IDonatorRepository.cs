using Cactus.Models.Database;

namespace Cactus.Infrastructure.Interfaces
{
    public interface IDonatorRepository:IBaseRepository<Donator>
    {
        Task<Donator> GetDonator(int targetId, int typeId, int userId);
    }
}
