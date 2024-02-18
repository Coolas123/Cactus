using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace Cactus.Infrastructure.Repositories
{
    public class UninterestingAuthorRepository : IUninterestingAuthorRepository
    {
        private readonly ApplicationDbContext dbContext;
        public UninterestingAuthorRepository(ApplicationDbContext dbContext) {
            this.dbContext = dbContext;
        }

        public async Task<bool> CreateAsync(UninterestingAuthor entity) {
            await dbContext.UninterestingAuthors.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public Task<bool> DeleteAsync(UninterestingAuthor entity) {
            throw new NotImplementedException();
        }

        public Task<UninterestingAuthor> GetAsync(int id) {
            throw new NotImplementedException();
        }

        public async Task<UninterestingAuthor> GetAuthorAsync(int userId, int authorId) {
            return await dbContext.UninterestingAuthors.FirstOrDefaultAsync(x => x.UserId == userId && x.AuthorId == authorId);
        }

        public async Task<IEnumerable<UninterestingAuthor>> GetAuthorsAsync(int userId) {
            return await dbContext.UninterestingAuthors.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<IEnumerable<UninterestingAuthor>> GetPagingAuthorsAsync(int userId, int authorPage, int pageSize) {
            return await dbContext.UninterestingAuthors.Include(x => x.Author).ThenInclude(x=>x.User).Where(x => x.UserId == userId).OrderBy(x => x.User.UserName).Skip((authorPage - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public Task<IEnumerable<UninterestingAuthor>> SelectAsync() {
            throw new NotImplementedException();
        }
    }
}
