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

        public Task<bool> DeleteAsync(Individual entity) {
            throw new NotImplementedException();
        }

        public async Task<Individual> GetAsync(int id) {
            return await dbContext.Individuals.FirstOrDefaultAsync(x => x.UserId == id);
        }

        public async Task<Individual> getByUrlPage(string urlPage) {
            return await dbContext.Individuals.FirstOrDefaultAsync(x=>x.UrlPage==urlPage);
        }

        public Task<IEnumerable<Individual>> SelectAsync() {
            throw new NotImplementedException();
        }
    }
}
