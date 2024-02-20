using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;

namespace Cactus.Infrastructure.Repositories
{
    public class PostTagRepository : IPostTagRepository
    {
        private readonly ApplicationDbContext dbContext;
        public PostTagRepository(ApplicationDbContext dbContext) {
            this.dbContext = dbContext;
        }

        public async Task<bool> AddTagsToPost(IEnumerable<PostTag> tags) {
            await dbContext.AddRangeAsync(tags);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public Task<bool> CreateAsync(PostTag entity) {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(PostTag entity) {
            throw new NotImplementedException();
        }

        public Task<PostTag> GetAsync(int id) {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PostTag>> SelectAsync() {
            throw new NotImplementedException();
        }
    }
}
