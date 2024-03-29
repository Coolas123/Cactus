using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;
using Cactus.Services.Interfaces;
using SportsStore.Models;

namespace Cactus.Services.Implementations
{
    public class AuthorSubscribeService : IAuthorSubscribeService
    {
        private readonly IAuthorSubscribeRepository subscribeRepository;
        public AuthorSubscribeService(IAuthorSubscribeRepository subscribeRepository) {
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

        public async Task<BaseResponse<PagingAuthorViewModel>> GetUserViewSubscribersAsync(int userId, int authorPage, int PageSize) {
            BaseResponse<IEnumerable<AuthorSubscribe>> subList = await GetPagingSubscribersAsync(userId, authorPage, PageSize);
            BaseResponse<IEnumerable<AuthorSubscribe>> allSub = await GetSubscribersAsync(userId);

            var response = new PagingAuthorViewModel();
            if (subList.StatusCode == 200) {
                response.Authors = subList.Data;
                response.SubscribesPagingInfo = new PagingInfo
                {
                    CurrentPage = authorPage,
                    ItemsPerPage = PageSize,
                    TotalItems = allSub.Data.Count()
                };
            }
            else {
                response.SubscribesPagingInfo = new PagingInfo
                {
                    Description = subList.Description
                };
            }
            return new BaseResponse<PagingAuthorViewModel>
            {
                Data = response,
                StatusCode=200
            };
        }

        public async Task<BaseResponse<AuthorSubscribe>> GetSubscribe(int userId, int authorId) {
            AuthorSubscribe subscribe = await subscribeRepository.GetSubscribeAsync(userId, authorId);
            if (subscribe == null) {
                return new BaseResponse<AuthorSubscribe>
                {
                    Description="Подписка отсутствует"
                };
            }
            return new BaseResponse<AuthorSubscribe>
            {
                Data= subscribe,
                StatusCode=200
            };
        }
    }
}
