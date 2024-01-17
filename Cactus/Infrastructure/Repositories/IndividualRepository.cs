using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;

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
            return true;
        }

        public Task<bool> DeleteAsync(Individual entity) {
            throw new NotImplementedException();
        }

        public Task<Individual> GetAsync(int id) {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Individual>> SelectAsync() {
            throw new NotImplementedException();
        }
    }
}
