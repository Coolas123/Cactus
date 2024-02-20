using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace Cactus.Infrastructure.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly ApplicationDbContext dbContext;
        public TagRepository(ApplicationDbContext dbContext) {
            this.dbContext = dbContext;
        }

        public Task<bool> CreateAsync(Tag entity) {
            throw new NotImplementedException();
        }

        public async Task<bool> CreateAsync(IEnumerable<Tag> tags) {
            await dbContext.Tags.AddRangeAsync(tags);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public Task<bool> DeleteAsync(Tag entity) {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Tag>> GetAllByNamesAsync(IEnumerable<string> tags) {
            return await dbContext.Tags.Where(p => tags.Contains(p.Name)).ToListAsync();
        }

        public Task<Tag> GetAsync(int id) {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Tag>> SelectAsync() {
            return await dbContext.Tags.Select(x => x).ToListAsync();
        }
    }
}
