using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;

namespace Cactus.Services.Interfaces
{
    public interface IDonationOptionService
    {
        Task<BaseResponse<IEnumerable<DonationOption>>> GetOptionsAsync(int authorId);
        Task<BaseResponse<bool>> AddOptionAsync(NewDonationOption model);
        Task<BaseResponse<DonationOption>> GetByPriceAsync(decimal price);
        Task<BaseResponse<bool>> PayGoalAsync(int id,decimal price);
    }
}
