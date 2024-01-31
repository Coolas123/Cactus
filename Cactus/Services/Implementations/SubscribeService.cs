using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Services.Interfaces;

namespace Cactus.Services.Implementations
{
    public class SubscribeService : ISubscribeService
    {
        private readonly ISubscribeRepository subscribeRepository;
        public SubscribeService(ISubscribeRepository subscribeRepository) {
            this.subscribeRepository = subscribeRepository;
        }

        public async Task<BaseResponse<AuthorSubscribe>> SubscribeToAuthor(int subcriberId, int authorId) {
            AuthorSubscribe sub = await subscribeRepository.GetSubscriptionAsync(subcriberId, authorId);
            if (sub != null) {
                return new BaseResponse<AuthorSubscribe>
                {
                    Description = "Подписка уже оформлена"
                };
            }
            sub = new AuthorSubscribe()
            {
                UserId = subcriberId,
                AuthorId = authorId,
            };
            await subscribeRepository.CreateAsync(sub);
            return new BaseResponse<AuthorSubscribe>
            {
                Data = sub,
                Description = "Подписка оформлена",
                StatusCode = 200
            };
        }

        public async Task<BaseResponse<IEnumerable<AuthorSubscribe>>> GetPagingSubscribersAsync(int subcriberId, int authorPage, int pageSize) {
            IEnumerable<AuthorSubscribe> subs = await subscribeRepository.GetPagingSubscribersAsync(subcriberId, authorPage, pageSize);
            if (subs.Count()==0) {
                return new BaseResponse<IEnumerable<AuthorSubscribe>>
                {
                    Description="Подписки отсутствуют"
                };
            }
            return new BaseResponse<IEnumerable<AuthorSubscribe>>
            {
                Data=subs,
                StatusCode=200
            };
        }

        public async Task<BaseResponse<IEnumerable<AuthorSubscribe>>> GetSubscribersAsync(int subcriberId) {
            return new BaseResponse<IEnumerable<AuthorSubscribe>>
            {
                Data = await subscribeRepository.GetSubscribersAsync(subcriberId)
            };
        }
    }
}
