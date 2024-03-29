using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace Cactus.Infrastructure.Repositories
{
    public class PostDonationOptionRepository : IPostDonationOptionRepository
    {
        private readonly ApplicationDbContext dbContext;
        public PostDonationOptionRepository(ApplicationDbContext dbContext) {
            this.dbContext = dbContext;
        }

        public async Task<bool> AddOptionToPostAsync(PostDonationOption entity) {
            await dbContext.PostDonationOptions.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public Task<bool> CreateAsync(PostDonationOption entity) {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(PostDonationOption entity) {
            throw new NotImplementedException();
        }

        public Task<PostDonationOption> GetAsync(int id) {
            throw new NotImplementedException();
        }

        public async Task<PostDonationOption> GetOptionAsync(int postId) {
            return await dbContext.PostDonationOptions.Include(x => x.DonationOption).FirstOrDefaultAsync(x => x.PostId == postId);
        }

        public Task<IEnumerable<PostDonationOption>> SelectAsync() {
            throw new NotImplementedException();
        }
    }
}