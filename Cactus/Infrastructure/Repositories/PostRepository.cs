using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;

namespace Cactus.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext dbContext;
        public PostRepository(ApplicationDbContext dbContext) {
            this.dbContext = dbContext;
        }

        public async Task<bool> CreateAsync(Post entity) {
            await dbContext.Posts.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public Task<bool> DeleteAsync(Post entity) {
            throw new NotImplementedException();
        }

        public Task<Post> GetAsync(int id) {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Post>> SelectAsync() {
            throw new NotImplementedException();
        }
    }
}
