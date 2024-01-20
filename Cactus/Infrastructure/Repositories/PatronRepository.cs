using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace Cactus.Infrastructure.Repositories
{
    public class PatronRepository:IPatronRepository
    {
        private readonly ApplicationDbContext dbContext;
        public PatronRepository(ApplicationDbContext dbContext) {
            this.dbContext = dbContext;
        }

        public async Task<bool> CreateAsync(Patron entity) {
            await dbContext.Patrons.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Patron entity) {
            dbContext.Patrons.Remove(entity);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Patron> GetAsync(int id) {
            return await dbContext.Patrons.FirstOrDefaultAsync(x=>x.UserId==id);
        }

        public Task<IEnumerable<Patron>> SelectAsync() {
            throw new NotImplementedException();
        }
    }
}
