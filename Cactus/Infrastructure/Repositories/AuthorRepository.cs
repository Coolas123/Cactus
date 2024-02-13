using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace Cactus.Infrastructure.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly ApplicationDbContext dbContext;
        public AuthorRepository(ApplicationDbContext dbContext) {
            this.dbContext = dbContext;
        }

        public async Task<bool> CreateAsync(Author entity) {
            await dbContext.Authors.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Author entity) {
            dbContext.Authors.Remove(entity);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Author> GetAsync(int id) {
            return await dbContext.Authors.FirstOrDefaultAsync(x => x.UserId == id);
        }

        public async Task<Author> GetByUrlPageAsync(string urlPage) {
            return await dbContext.Authors.FirstOrDefaultAsync(x=>x.UrlPage==urlPage);
        }

        public async Task<Author> GetUserByUrlPageAsync(string urlPage) {
            return await dbContext.Authors.Include(x => x.User).FirstOrDefaultAsync(x => x.UrlPage == urlPage);
        }

        public Task<IEnumerable<Author>> SelectAsync() {
            throw new NotImplementedException();
        }
    }
}
