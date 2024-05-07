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

        public async Task<Post> GetLastAsync(DateTime time) {
            return await dbContext.Posts.Where(x=>x.Created==time).FirstAsync();
        }

        public async Task<IEnumerable<Post>> GetPagingPostsAsync(int authorId, int postPage, int pageSize) {
            return await dbContext.Posts.Where(x => x.UserId == authorId).Skip((postPage - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<Post> GetPostByIdAsync(int postId) {
            return await dbContext.Posts.FirstOrDefaultAsync(x=>x.Id==postId);
        }

        public async Task<IEnumerable<Post>> GetPostsAsync(int authorId) {
            return await dbContext.Posts.Where(x => x.UserId == authorId).ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetPostsByTitleAsync(IEnumerable<string> titles) {
            return await dbContext.Posts.Where(x=>titles.Contains(x.Title)).ToListAsync();
        }

        public async Task<Post> GetPostWithUserAsync(int postId) {
            return await dbContext.Posts.Include(x=>x.User).ThenInclude(x=>x.User).FirstOrDefaultAsync(x => x.Id == postId);
        }

        public Task<IEnumerable<Post>> SelectAsync() {
            throw new NotImplementedException();
        }
    }
}
