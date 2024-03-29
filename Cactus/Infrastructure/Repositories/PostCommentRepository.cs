using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace Cactus.Infrastructure.Repositories
{
    public class PostCommentRepository : IPostCommentRepository
    {
        private readonly ApplicationDbContext dbContext;
        public PostCommentRepository(ApplicationDbContext dbContext) {
            this.dbContext = dbContext;
        }

        public async Task<bool> CreateAsync(PostComment entity) {
            await dbContext.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public Task<bool> DeleteAsync(PostComment entity) {
            throw new NotImplementedException();
        }

        public Task<PostComment> GetAsync(int id) {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PostComment>> GetCommentsAsync(int postId) {
            return await dbContext.PostComments.Include(x=>x.User).Where(x => x.PostId==postId).ToListAsync();
        }

        public Task<IEnumerable<PostComment>> SelectAsync() {
            throw new NotImplementedException();
        }
    }
}
