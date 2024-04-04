using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;
using Cactus.Services.Interfaces;
using System.Threading;

namespace Cactus.Services.Implementations
{
    public class DonatorService : IDonatorService
    {
        private readonly IDonatorRepository donatorRepository;
        public DonatorService(IDonatorRepository donatorRepository) {
            this.donatorRepository = donatorRepository;
        }
        public async Task<BaseResponse<IEnumerable<Donator>>> GetPostDonator(int postId, int DonationOptinoId, int userId) {
            IEnumerable<Donator> donator = await donatorRepository.GetPostDonator(postId, DonationOptinoId, userId);
            if (donator == null || !donator.Any()) {
                return new BaseResponse<IEnumerable<Donator>>
                {
                    Description="Пост недоступен"
                };
            }
            return new BaseResponse<IEnumerable<Donator>>
            {
                Data = donator,
                StatusCode = 200
            };
        }

        public async Task<BaseResponse<Dictionary<int, decimal>>> GetCollectedSumOfGoals(List<int> optionId) {
            Dictionary<int,decimal> donators = await donatorRepository.GetCollectedSumOfGoalsAsync(optionId);
            if (donators == null) {
                return new BaseResponse<Dictionary<int, decimal>>
                {
                    Description = ""
                };
            }
            return new BaseResponse<Dictionary<int, decimal>>
            {
                Data = donators,
                StatusCode = 200
            };
        }

        public async Task<BaseResponse<IEnumerable<Donator>>> GetDonators(int userId) {
            IEnumerable<Donator> donators = await donatorRepository.GetDonatorsAsync(userId);
            if (!donators.Any()) {
                return new BaseResponse<IEnumerable<Donator>>
                {
                    Description="Пожертвования отсутствуют"
                };
            }
            return new BaseResponse<IEnumerable<Donator>>
            {
                Data= donators,
                StatusCode = 200
            };
        }

        public async Task<BaseResponse<IEnumerable<Donator>>> GetDonators(int userId, DateTime dateFrom, DateTime dateTo) {
            IEnumerable<Donator> donators = await donatorRepository.GetDonatorsAsync(userId);
            if (!donators.Any()) {
                return new BaseResponse<IEnumerable<Donator>>
                {
                    Description = "Пожертвования отсутствуют"
                };
            }
            return new BaseResponse<IEnumerable<Donator>>
            {
                Data = donators,
                StatusCode = 200
            };
        }

        public async Task<BaseResponse<bool>> AddDonator(DonatorViewModel model) {
            var donator = new Donator
            {
                UserId = model.UserId,
                DonationOptionId = model.DonationOptionId,
                DonationTargetTypeId = model.DonationTargetTypeId,
                TransactionId = model.TransactionId
            };
            try {
                await donatorRepository.CreateAsync(donator);
            }
            catch {
                return new BaseResponse<bool>
                {
                    Description = "Не удолось сохранить"
                };
            }
            return new BaseResponse<bool>
            {
                Data = true,
                StatusCode = 200
            };
        }

        public async Task<BaseResponse<Donator>> GetLastDonator(DateTime created, int userId) {
            Donator donator = await donatorRepository.GetLastDonator(created, userId);
            if (donator == null) {
                return new BaseResponse<Donator>
                {
                    Description="Не удалось найти запись"
                };
            }
            return new BaseResponse<Donator>
            {
                Data = donator,
                StatusCode = 200
            };
        }
    }
}
