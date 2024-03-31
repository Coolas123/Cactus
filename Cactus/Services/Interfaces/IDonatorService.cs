using Cactus.Models.Database;
using Cactus.Models.Responses;

namespace Cactus.Services.Interfaces
{
    public interface IDonatorService
    {
        Task<BaseResponse<Donator>> GetDonator(int targetId, int typeId, int userId);
        Task<BaseResponse<IEnumerable<Donator>>> GetDonators(int userId);
        Task<BaseResponse<IEnumerable<Donator>>> GetDonators(int userId, DateTime dateFrom, DateTime dateTo);
        Task<BaseResponse<Dictionary<int, decimal>>> GetCollectedSumOfGoals(List<int> optionId);

    }
}
