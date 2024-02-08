using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;

namespace Cactus.Infrastructure.Repositories
{
    public class PostCategoryRepository : IPostCategoryRepository
    {
        private readonly ApplicationDbContext dbContext;
        public PostCategoryRepository(ApplicationDbContext dbContext) {
            this.dbContext = dbContext;
        }

        public async Task<bool> CreateAsync(PostCategory entity) {
            await dbContext.PostCategories.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public Task<bool> DeleteAsync(PostCategory entity) {
            throw new NotImplementedException();
        }

        public Task<PostCategory> GetAsync(int id) {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PostCategory>> SelectAsync() {
            throw new NotImplementedException();
        }
    }
}
