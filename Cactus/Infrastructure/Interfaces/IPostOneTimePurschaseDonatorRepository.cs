using Cactus.Models.Database;
using Cactus.Models.Responses;

namespace Cactus.Infrastructure.Interfaces
{
    public interface IPostOneTimePurschaseDonatorRepository:IBaseRepository<PostOneTimePurschaseDonator>
    {
        Task<PostOneTimePurschaseDonator> GetDonator(int postId, int userId);
    }
}
