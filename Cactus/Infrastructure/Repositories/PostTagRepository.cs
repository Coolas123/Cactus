using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IEnumerable<Author>> GetAuthorsByTagsAsync(IEnumerable<Tag> tags) {
            return await dbContext.PostTags
                .Where(x => tags
                    .Select(p => p.Id)
                    .Contains(x.TagId))
                .Include(x => x.Post)
                .ThenInclude(a => a.User)
                .ThenInclude(a => a.User)
                .Select(a => new Author
                {
                    UserId = a.Post.UserId,
                    UrlPage = a.Post.User.UrlPage,
                    User = a.Post.User.User
                })
                .Distinct()
                .ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetPostsByTagsAsync(IEnumerable<Tag> tags) {
            return await dbContext.PostTags
                .Where(x => tags
                    .Select(p => p.Id)
                    .Contains(x.TagId))
                .Include(x => x.Post)
                .Select(x => new Post
                {
                    Id=x.Post.Id,
                    UserId=x.Post.Id,
                    Title=x.Post.Title,
                    Description=x.Post.Description,
                    Created=x.Post.Created,
                })
                .Distinct()
                .ToListAsync();
        }

        public async Task<IEnumerable<Tag>> GetPostTagsAsync(int postId) {
            return await dbContext.PostTags.Include(x => x.Tag).Where(x=>x.PostId==postId).Select(x=>new Tag { Name=x.Tag.Name,Id=x.Tag.Id}).ToListAsync();
        }

        public Task<IEnumerable<PostTag>> SelectAsync() {
            throw new NotImplementedException();
        }
    }
}
