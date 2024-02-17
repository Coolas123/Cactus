using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;

namespace Cactus.Infrastructure.Repositories
{
    public class GrayListAuthorRepository : IGrayListAuthorRepository
    {
        private readonly ApplicationDbContext dbContext;
        public GrayListAuthorRepository(ApplicationDbContext dbContext) {
            this.dbContext = dbContext;
        }

        public Task<bool> CreateAsync(GrayListAuthor entity) {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(GrayListAuthor entity) {
            throw new NotImplementedException();
        }

        public Task<GrayListAuthor> GetAsync(int id) {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GrayListAuthor>> SelectAsync() {
            throw new NotImplementedException();
        }
    }
}
