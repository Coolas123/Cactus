using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;
using Cactus.Services.Interfaces;
using SportsStore.Models;

namespace Cactus.Services.Implementations
{
    public class ProjectSubscribeService : IProjectSubscribeService
    {
        private readonly IProjectSubscribeRepository projectSubscribeRepository;
        public ProjectSubscribeService(IProjectSubscribeRepository projectSubscribeRepository) {
            this.projectSubscribeRepository = projectSubscribeRepository;
        }

        public async Task<BaseResponse<IEnumerable<ProjectSubscribe>>> GetPagingProjectsAsync(int subcriberId, int authorPage, int pageSize) {
            IEnumerable<ProjectSubscribe> subs = await projectSubscribeRepository.GetPagingSubscribersAsync(subcriberId, authorPage, pageSize);
            if (subs.Count() == 0) {
                return new BaseResponse<IEnumerable<ProjectSubscribe>>
                {
                    Description = "Подписки отсутствуют"
                };
            }
            return new BaseResponse<IEnumerable<ProjectSubscribe>>
            {
                Data = subs,
                StatusCode = 200
            };
        }

        public async Task<BaseResponse<IEnumerable<ProjectSubscribe>>> GetProjectsAsync(int subcriberId) {
            return new BaseResponse<IEnumerable<ProjectSubscribe>>
            {
                Data = await projectSubscribeRepository.GetSubscribersAsync(subcriberId)
            };
        }

        public async Task<BaseResponse<PagingPatronViewModel>> GetUserViewProjectsAsync(int userId, int authorPage, int PageSize) {
            BaseResponse<IEnumerable<ProjectSubscribe>> subList = await GetPagingProjectsAsync(userId, authorPage, PageSize);
            BaseResponse<IEnumerable<ProjectSubscribe>> allSub = await GetProjectsAsync(userId);

            var response = new PagingPatronViewModel();
            if (subList.StatusCode == 200) {
                response.ProjectSubscribe = subList.Data;
                response.ProjectsPagingInfo = new PagingInfo
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
            return new BaseResponse<PagingPatronViewModel>
            {
                Data = response,
                StatusCode = 200
            };
        }

        public async Task<BaseResponse<ProjectSubscribe>> SubscribeToProject(int subcriberId, int projectId) {
            ProjectSubscribe sub = await projectSubscribeRepository.GetSubscriptionAsync(subcriberId, projectId);
            if (sub != null) {
                return new BaseResponse<ProjectSubscribe>
                {
                    Description = "Подписка уже оформлена"
                };
            }
            sub = new ProjectSubscribe()
            {
                UserId = subcriberId,
                ProjectId = projectId,
            };
            await projectSubscribeRepository.CreateAsync(sub);
            return new BaseResponse<ProjectSubscribe>
            {
                Data = sub,
                Description = "Подписка оформлена",
                StatusCode = 200
            };
        }
    }
}
