using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace Cactus.Infrastructure.Repositories
{
    public class IndividualRepository : IIndividualRepository
    {
        private readonly ApplicationDbContext dbContext;
        public IndividualRepository(ApplicationDbContext dbContext) {
            this.dbContext = dbContext;
        }

        public async Task<bool> CreateAsync(Individual entity) {
            await dbContext.Individuals.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Individual entity) {
            dbContext.Individuals.Remove(entity);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Individual> GetAsync(int id) {
            return await dbContext.Individuals.FirstOrDefaultAsync(x => x.UserId == id);
        }

        public async Task<Individual> GetByUrlPageAsync(string urlPage) {
            return await dbContext.Individuals.FirstOrDefaultAsync(x=>x.UrlPage==urlPage);
        }

        public async Task<Individual> GetUserByUrlPageAsync(string urlPage) {
            return await dbContext.Individuals.Include(x => x.User).FirstOrDefaultAsync(x => x.UrlPage == urlPage);
        }

        public Task<IEnumerable<Individual>> SelectAsync() {
            throw new NotImplementedException();
        }
    }
}
