using Cactus.Models.Database;

namespace Cactus.Infrastructure.Interfaces
{
    public interface IProjectRepository:IBaseRepository<Project>
    {
        Task<IEnumerable<Project>> GetPagingProjectsAsync(int authorId, int projectPage, int pageSize);
        Task<IEnumerable<Project>> GetProjectsAsync(int authorId);
    }
}
