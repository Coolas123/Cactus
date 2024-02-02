using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IEnumerable<Post>> GetPagingPostsAsync(int authorId, int postPage, int pageSize) {
            return await dbContext.Posts.Where(x => x.Id == authorId).OrderBy(x => x.Title).Skip((postPage - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetPostsAsync(int authorId) {
            return await dbContext.Posts.Where(x => x.Id == authorId).ToListAsync();
        }

        public Task<IEnumerable<Post>> SelectAsync() {
            throw new NotImplementedException();
        }
    }
}
