using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace Cactus.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext dbContext;
        public CategoryRepository(ApplicationDbContext dbContext) {
            this.dbContext = dbContext;
        }

        public Task<bool> CreateAsync(Category entity) {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Category entity) {
            throw new NotImplementedException();
        }

        public Task<Category> GetAsync(int id) {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Category>> SelectAsync() {
            return await dbContext.Categories.Select(x => x).ToListAsync();
        }
    }
}
