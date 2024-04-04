using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace Cactus.Infrastructure.Repositories
{
    public class PostOneTimePurschaseDonatorRepository : IPostOneTimePurschaseDonatorRepository
    {
        private readonly ApplicationDbContext dbContext;
        public PostOneTimePurschaseDonatorRepository(ApplicationDbContext dbContext) {
            this.dbContext = dbContext;
        }

        public async Task<bool> CreateAsync(PostOneTimePurschaseDonator entity) {
            await dbContext.PostOneTimePurschaseDonators.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public Task<bool> DeleteAsync(PostOneTimePurschaseDonator entity) {
            throw new NotImplementedException();
        }

        public Task<PostOneTimePurschaseDonator> GetAsync(int id) {
            throw new NotImplementedException();
        }

        public async Task<PostOneTimePurschaseDonator> GetDonator(int postId, int userId) {
            return await dbContext.PostOneTimePurschaseDonators
                .Include(x=>x.Donator)
                .FirstOrDefaultAsync(x=>x.PostId==postId&&x.Donator.UserId==userId);
        }

        public Task<IEnumerable<PostOneTimePurschaseDonator>> SelectAsync() {
            throw new NotImplementedException();
        }
    }
}
