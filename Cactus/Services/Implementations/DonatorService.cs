using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Services.Interfaces;

namespace Cactus.Services.Implementations
{
    public class DonatorService : IDonatorService
    {
        private readonly IDonatorRepository donatorRepository;
        public DonatorService(IDonatorRepository donatorRepository) {
            this.donatorRepository = donatorRepository;
        }
        public async Task<BaseResponse<Donator>> GetDonator(int targetId, int typeId, int userId) {
            Donator donator = await donatorRepository.GetDonator(targetId, typeId, userId);
            if (donator == null) {
                return new BaseResponse<Donator>
                {
                    Description="Пост недоступен"
                };
            }
            return new BaseResponse<Donator>
            {
                Data = donator,
                StatusCode = 200
            };
        }

        public async Task<BaseResponse<Dictionary<int, decimal>>> GetCollectedSumOfGoals(List<int> optionId) {
            Dictionary<int,decimal> donators = await donatorRepository.GetCollectedSumOfGoals(optionId);
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
    }
}
