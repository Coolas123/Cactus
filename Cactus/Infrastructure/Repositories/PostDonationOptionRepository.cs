using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Cactus.Infrastructure.Repositories
{
    public class PostDonationOptionRepository : IPostDonationOptionRepository
    {
        private readonly ApplicationDbContext dbContext;
        public PostDonationOptionRepository(ApplicationDbContext dbContext) {
            this.dbContext = dbContext;
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

        public async Task<PostDonationOption> GetOption(int postId) {
            return await dbContext.PostDonationOptions.FirstOrDefaultAsync(x=>x.PostId==postId);
        }

        public Task<IEnumerable<PostDonationOption>> SelectAsync() {
            throw new NotImplementedException();
        }
    }
}
