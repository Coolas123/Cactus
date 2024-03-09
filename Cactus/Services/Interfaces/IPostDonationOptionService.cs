using Cactus.Models.Database;
using Cactus.Models.Responses;

namespace Cactus.Services.Interfaces
{
    public interface IPostDonationOptionService
    {
        Task<BaseResponse<PostDonationOption>> GetOption(int postId);
    }
}
