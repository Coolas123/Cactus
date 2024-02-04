using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;

namespace Cactus.Services.Interfaces
{
    public interface IProjectSubscribeService
    {
        Task<BaseResponse<ProjectSubscribe>> SubscribeToProject(int subcriberId, int projectId);
        Task<BaseResponse<IEnumerable<ProjectSubscribe>>> GetPagingProjectsAsync(int subcriberId, int authorPage, int pageSize);
        Task<BaseResponse<IEnumerable<ProjectSubscribe>>> GetProjectsAsync(int subcriberId);
        Task<BaseResponse<PagingPatronViewModel>> GetUserViewProjectsAsync(int userId, int authorPage, int PageSize);
    }
}
