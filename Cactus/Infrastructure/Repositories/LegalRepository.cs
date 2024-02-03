using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace Cactus.Infrastructure.Repositories
{
    public class LegalRepository : ILegalRepository
    {
        private readonly ApplicationDbContext dbContext;
        public LegalRepository(ApplicationDbContext dbContext) {
            this.dbContext = dbContext;
        }
        public async Task<bool> CreateAsync(Legal entity) {
            await dbContext.Legals.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public Task<bool> DeleteAsync(Legal entity) {
            throw new NotImplementedException();
        }

        public async Task<Legal> GetAsync(int id) {
            return await dbContext.Legals.FirstOrDefaultAsync(x=>x.UserId==id);
        }

        public async Task<Legal> GetByUrlPageAsync(string urlPage) {
            return await dbContext.Legals.FirstOrDefaultAsync(x=>x.UrlPage== urlPage);
        }

        public Task<IEnumerable<Legal>> SelectAsync() {
            throw new NotImplementedException();
        }
    }
}
