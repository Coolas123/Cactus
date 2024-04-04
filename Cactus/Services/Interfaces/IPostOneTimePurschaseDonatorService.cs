using Cactus.Models.Database;
using Cactus.Models.Responses;

namespace Cactus.Services.Interfaces
{
    public interface IPostOneTimePurschaseDonatorService
    {
        Task<BaseResponse<bool>> AddDonator(int postId, int donatorId);
        Task<BaseResponse<PostOneTimePurschaseDonator>> GetDonator(int postId, int userId);
    }
}
