using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;
using Cactus.Services.Interfaces;

namespace Cactus.Services.Implementations
{
    public class ComplainService : IComplainService
    {
        private readonly IComplainRepository complainRepository;
        public ComplainService(IComplainRepository complainRepository) {
            this.complainRepository = complainRepository;
        }

        public async Task<BaseResponse<IEnumerable<ComplainViewModel>>> GetNotReviewedComplains(int minCountComplains, DateTime date) {
            var complains = await complainRepository.GetNotReviewedComplains(minCountComplains, date.ToUniversalTime());
            if (complains != null) {
                return new BaseResponse<IEnumerable<ComplainViewModel>>
                {
                    Data = complains,
                    StatusCode = 200
                };
            }
            return new BaseResponse<IEnumerable<ComplainViewModel>>
            {
                Description="Жалобы не найдены"
            };
        }
    }
}
