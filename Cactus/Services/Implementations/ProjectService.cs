using Cactus.Infrastructure.Interfaces;
using Cactus.Infrastructure.Repositories;
using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;
using Cactus.Services.Interfaces;
using SportsStore.Models;

namespace Cactus.Services.Implementations
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository projectRepository;
        public ProjectService(IProjectRepository projectRepository) {
            this.projectRepository = projectRepository;
        }

        public async Task<BaseResponse<Project>> GetAsync(int authorId) {
            Project project = await projectRepository.GetAsync(authorId);
            if (project == null) {
                return new BaseResponse<Project>{
                    Description="Проект не найден"
                };
            }
            return new BaseResponse<Project>
            {
                Data = project,
                StatusCode = 200
            };
        }

        public async Task<BaseResponse<IEnumerable<Project>>> GetPagingProjectsAsync(int authorId, int projectPage, int pageSize) {
            IEnumerable<Project> projects = await projectRepository.GetPagingProjectsAsync(authorId, projectPage, pageSize);
            if (projects.Count() == 0) {
                return new BaseResponse<IEnumerable<Project>>
                {
                    Description = "Проекты отсутствуют"
                };
            }
            return new BaseResponse<IEnumerable<Project>>
            {
                Data = projects,
                StatusCode = 200
            };
        }

        public async Task<BaseResponse<IEnumerable<Project>>> GetProjectsAsync(int authorId) {
            return new BaseResponse<IEnumerable<Project>>
            {
                Data = await projectRepository.GetProjectsAsync(authorId)
            };
        }

        public async Task<BaseResponse<PagingProjectViewModel>> GetUserViewProjectsAsync(int authorId, int projectPage, int PageSize) {
            BaseResponse<IEnumerable<Project>> postList = await GetPagingProjectsAsync(authorId, projectPage, PageSize);
            BaseResponse<IEnumerable<Project>> allPost = await GetProjectsAsync(authorId);

            var response = new PagingProjectViewModel();
            if (postList.StatusCode == 200) {
                response.Projects = postList.Data;
                response.ProjectsPagingInfo = new PagingInfo
                {
                    CurrentPage = projectPage,
                    ItemsPerPage = PageSize,
                    TotalItems = allPost.Data.Count()
                };
            }
            else {
                response.ProjectsPagingInfo = new PagingInfo
                {
                    Description = postList.Description
                };
            }
            return new BaseResponse<PagingProjectViewModel>
            {
                Data = response,
                StatusCode = 200
            };
        }
    }
}
