using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;
using Cactus.Services.Interfaces;

namespace Cactus.Services.Implementations
{
    public class PaidAuthorSubscribeService : IPaidAuthorSubscribeService
    {
        private readonly IPaidAuthorSubscribeRepository paidAuthorSubscribeRepository;
        public PaidAuthorSubscribeService(IPaidAuthorSubscribeRepository paidAuthorSubscribeRepository) {
            this.paidAuthorSubscribeRepository = paidAuthorSubscribeRepository;
        }

        public async Task<BaseResponse<IEnumerable<PaidAuthorSubscribe>>> GetCurrentSubscribes(int authorId, int donatorId) {
            IEnumerable<PaidAuthorSubscribe> subs = await paidAuthorSubscribeRepository.GetCurrentSubscribes(authorId, donatorId);
            if (!subs.Any()) {
                return new BaseResponse<IEnumerable<PaidAuthorSubscribe>>
                {
                    Description="Подписки отсутствуют"
                };
            }
            return new BaseResponse<IEnumerable<PaidAuthorSubscribe>>
            {
                Data = subs,
                StatusCode=200
            };
        }

        public async Task<BaseResponse<bool>> SubscribeToAuthor(NewPaidSubscribeViewModel model) {
            var sub = new PaidAuthorSubscribe
            {
                DonatorId=model.DonatorId,
                StartDate=model.StartDate.ToUniversalTime(),
                EndDate=model.EndDate.ToUniversalTime(),
            };
            try {
                await paidAuthorSubscribeRepository.SubscribeToAuthorAsync(sub);
            }
            catch{
                return new BaseResponse<bool>
                {
                    Description="Не удалось офрмить подписку"
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
