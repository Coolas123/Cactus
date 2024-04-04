using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;

namespace Cactus.Services.Interfaces
{
    public interface IDonatorService
    {
        Task<BaseResponse<IEnumerable<Donator>>> GetPostDonator(int postId, int DonationOptinoId, int userId);
        Task<BaseResponse<bool>> AddDonator(DonatorViewModel model);
        Task<BaseResponse<IEnumerable<Donator>>> GetDonators(int userId, DateTime dateFrom, DateTime dateTo);
        Task<BaseResponse<IEnumerable<Donator>>> GetDonators(int userId);
        Task<BaseResponse<Dictionary<int, decimal>>> GetCollectedSumOfGoals(List<int> optionId);
        Task<BaseResponse<Donator>> GetLastDonator(DateTime created, int userId);

    }
}
