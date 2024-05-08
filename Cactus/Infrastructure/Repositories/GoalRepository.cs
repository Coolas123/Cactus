using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace Cactus.Infrastructure.Repositories
{
    public class GoalRepository : IGoalRepository
    {
        private readonly ApplicationDbContext dbContext;
        public GoalRepository(ApplicationDbContext dbContext) {
            this.dbContext = dbContext;
        }

        public async Task<bool> CreateAsync(Goal entity) {
            await dbContext.Golas.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public Task<bool> DeleteAsync(Goal entity) {
            throw new NotImplementedException();
        }

        public async Task<Goal> GetAsync(int id) {
            return await dbContext.Golas.FirstOrDefaultAsync(x=>x.DonationOptionId==id);
        }

        public async Task<IEnumerable<Goal>> GetWorkGoalsAsync(int authorId) {
            return await dbContext.Golas
                .Include(x=>x.DonationOption)
                .Where(x=>x.DonationOption.AuthorId == authorId &&
                !x.IsDone)
                .ToListAsync();
        }

        public async Task<bool> UpdateAsync(Goal entiry) {
            dbContext.Golas.Update(entiry);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public Task<IEnumerable<Goal>> SelectAsync() {
            throw new NotImplementedException();
        }
    }
}
