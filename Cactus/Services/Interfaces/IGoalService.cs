using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;

namespace Cactus.Services.Interfaces
{
    public interface IGoalService
    {
        Task<BaseResponse<bool>> ReplenishGoal(int goalId, decimal sended);
        Task<BaseResponse<IEnumerable<Goal>>> GetWorkGoals(int authorId);
        Task<BaseResponse<bool>> CreateGoal(NewDonationOptionViewModel model);
        Task<BaseResponse<bool>> DoneGoal(int goalId);
        Task<BaseResponse<Goal>> GetAsync(int goalId);
    }
}
