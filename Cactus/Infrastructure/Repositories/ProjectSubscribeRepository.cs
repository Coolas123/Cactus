using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace Cactus.Infrastructure.Repositories
{
    public class ProjectSubscribeRepository : IProjectSubscribeRepository
    {
        private readonly ApplicationDbContext dbContext;
        public ProjectSubscribeRepository(ApplicationDbContext dbContext) {
            this.dbContext = dbContext;
        }

        public async Task<bool> CreateAsync(ProjectSubscribe entity) {
            await dbContext.ProjectSubscribes.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public Task<bool> DeleteAsync(ProjectSubscribe entity) {
            throw new NotImplementedException();
        }

        public Task<ProjectSubscribe> GetAsync(int id) {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProjectSubscribe>> GetPagingSubscribersAsync(int subcriberId, int projectPage, int pageSize) {
            return await dbContext.ProjectSubscribes.Include(x => x.Project).Where(x => x.UserId == subcriberId).OrderBy(x => x.Project.Title).Skip((projectPage - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<IEnumerable<ProjectSubscribe>> GetSubscribersAsync(int subcriberId) {
            return await dbContext.ProjectSubscribes.Where(x => x.UserId == subcriberId).ToListAsync();
        }

        public async Task<ProjectSubscribe> GetSubscriptionAsync(int subcriberId, int projectId) {
            return await dbContext.ProjectSubscribes.FirstOrDefaultAsync(x => x.UserId == subcriberId && x.ProjectId == projectId);
        }

        public Task<IEnumerable<ProjectSubscribe>> SelectAsync() {
            throw new NotImplementedException();
        }
    }
}
