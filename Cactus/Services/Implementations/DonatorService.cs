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
                    Description="Пост не доступен"
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
