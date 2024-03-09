using Cactus.Models.Database;

namespace Cactus.Infrastructure.Interfaces
{
    public interface IDonationOptionRepository:IBaseRepository<DonationOption>
    {
        Task<IEnumerable<DonationOption>> GetOptionsAsync(int authorId);
        Task<DonationOption> GetByPriceAsync(decimal price);
    }
}
