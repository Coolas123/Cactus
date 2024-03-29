using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Cactus.Models.Enums;
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

        public async Task<BaseResponse<IEnumerable<Complain>>> GetNotReviewedComplains(DateTime date) {
            var complains = await complainRepository.GetNotReviewedComplains(date.ToUniversalTime());
            if (complains != null) {
                return new BaseResponse<IEnumerable<Complain>>
                {
                    Data = complains,
                    StatusCode = 200
                };
            }
            return new BaseResponse<IEnumerable<Complain>>
            {
                Description="Жалобы не найдены"
            };
        }

        public async Task<BaseResponse<bool>> AddComplain(NewComplainViewModel model) {
            Complain complain = new Complain
            {
                SenderId = model.SenderId,
                ComplainTargetTypeId=model.ComplainTargetTypeId,
                ComplainTargetId=model.ComplainTargetId,
                Description = model.Description,
                Created = model.Created.ToUniversalTime(),
                ComplainStatusId = (int)Models.Enums.ComplainStatus.NotReviewed,
                ComplainTypeId = model.ComplainTypeId
            };
            try {
                await complainRepository.AddComplain(complain);
            }
            catch (Exception ex) {
                return new BaseResponse<bool>
                {
                    Description = "Не удалось создать жалобу"
                };
            }
            return new BaseResponse<bool>
            {
                StatusCode = 200,
                Data = true
            };
        }
    }
}
