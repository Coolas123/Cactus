using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;

namespace Cactus.Services.Interfaces
{
    public interface IProjectService
    {
        Task<BaseResponse<IEnumerable<Project>>> GetPagingProjectsAsync(int authorId, int projectPage, int pageSize);
        Task<BaseResponse<IEnumerable<Project>>> GetProjectsAsync(int authorId);
        Task<BaseResponse<PagingProjectViewModel>> GetUserViewProjectsAsync(int authorId, int projectPage, int PageSize);
        Task<BaseResponse<Project>> GetAsync(int authorId);
    }
}
