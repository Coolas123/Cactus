using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;
using Cactus.Services.Interfaces;

namespace Cactus.Services.Implementations
{
    public class GoalService : IGoalService
    {
        private readonly IGoalRepository goalRepository;
        private readonly IDonationOptionService donationOptionService;
        public GoalService(IGoalRepository goalRepository, IDonationOptionService donationOptionService) {
            this.goalRepository = goalRepository;
            this.donationOptionService = donationOptionService;
        }

        public async Task<BaseResponse<bool>> CreateGoal(NewDonationOptionViewModel model) {
            await donationOptionService.AddOptionAsync(model);
            BaseResponse<DonationOption> option = await donationOptionService.GetLastAsync(model.AuthorId);
            if (option.StatusCode != 200) {
                return new BaseResponse<bool>
                {
                    Description="Не удалось создать цель"
                };
            }
            var goal = new Goal
            {
                DonationOptionId = option.Data.Id
            };
            try {
                await goalRepository.CreateAsync(goal);
            }
            catch {
                return new BaseResponse<bool>
                {
                    Description = "Не удалось создать цель"
                };
            }
            return new BaseResponse<bool>
            {
                Data=true,
                StatusCode=200
            };
        }

        public async Task<BaseResponse<bool>> DoneGoal(int goalId) {
            BaseResponse<Goal> goal = await GetAsync(goalId);
            if(goal.StatusCode != 200) {
                return new BaseResponse<bool>
                {
                    Description = "Не удалось закрыть цель"
                };
            }
            goal.Data.IsDone=true;
            try {
                await goalRepository.UpdateAsync(goal.Data);
            }
            catch {
                return new BaseResponse<bool>
                {
                    Description = "Не удалось закрыть цель"
                };
            }
            return new BaseResponse<bool>
            {
                Data=true,
                StatusCode=200,
                Description="Цель успешно закрыта"
            };
        }

        public async Task<BaseResponse<Goal>> GetAsync(int goalId) {
            Goal goal = await goalRepository.GetAsync(goalId);
            if (goal == null) {
                return new BaseResponse<Goal>
                {
                    Description = "Цель не найдена"
                };
            }
            return new BaseResponse<Goal>
            {
                Data=goal,
                StatusCode=200
            };
        }

        public async Task<BaseResponse<IEnumerable<Goal>>> GetWorkGoals(int authorId) {
            IEnumerable<Goal> goals = await goalRepository.GetWorkGoalsAsync(authorId);
            if(goals == null) {
                return new BaseResponse<IEnumerable<Goal>>
                {
                    Description="Цели отсутствуют"
                };
            }
            return new BaseResponse<IEnumerable<Goal>>
            {
                Data=goals,
                StatusCode=200
            };
        }

        public async Task<BaseResponse<bool>> ReplenishGoal(int goalId, decimal sended) {
            Goal goal = await goalRepository.GetAsync(goalId);
            goal.TotalAmount += sended;
            try {
                await goalRepository.UpdateAsync(goal);
            }
            catch {
                return new BaseResponse<bool>
                {
                    Description="Не удалось пополнить цель"
                };
            }
            return new BaseResponse<bool>
            {
                Data=true,
                StatusCode=200
            };
        }
    }
}
