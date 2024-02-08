using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace Cactus.Infrastructure.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDbContext dbContext;
        public ProjectRepository(ApplicationDbContext dbContext) {
            this.dbContext = dbContext;
        }

        public Task<bool> CreateAsync(Project entity) {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Project entity) {
            throw new NotImplementedException();
        }

        public async Task<Project> GetAsync(int id) {
            return await dbContext.Projects.FirstOrDefaultAsync(x=>x.UserId==id);
        }

        public async Task<IEnumerable<Project>> GetPagingProjectsAsync(int authorId, int projectPage, int pageSize) {
            return await dbContext.Projects.Where(x => x.UserId == authorId).Skip((projectPage - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<IEnumerable<Project>> GetProjectsAsync(int authorId) {
            return await dbContext.Projects.Where(x => x.UserId == authorId).ToListAsync();
        }

        public Task<IEnumerable<Project>> SelectAsync() {
            throw new NotImplementedException();
        }
    }
}
