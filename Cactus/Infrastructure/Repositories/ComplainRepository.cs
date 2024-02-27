using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Cactus.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Cactus.Infrastructure.Repositories
{
    public class ComplainRepository : IComplainRepository
    {
        private readonly ApplicationDbContext dbContext;
        public ComplainRepository(ApplicationDbContext dbContext) {
            this.dbContext = dbContext;
        }

        public Task<bool> CreateAsync(Complain entity) {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Complain entity) {
            throw new NotImplementedException();
        }

        public Task<Complain> GetAsync(int id) {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ComplainViewModel>> GetNotReviewedComplains(int minCountComplains, DateTime date) {
            return await dbContext.Complains
                .Where(
                    x => x.Created >= date.ToUniversalTime()&&
                    x.ComplainStatusId==(int)Models.Enums.ComplainStatus.NotReviewed
                )
                .GroupBy(x => new { x.PostId, x.UserId, x.CommentId })
                .Where(x => x.Count()>= minCountComplains)
                .Select(x => new ComplainViewModel { 
                    Complains = x.ToList() 
                })
                .ToListAsync();
        }

        public Task<IEnumerable<Complain>> SelectAsync() {
            throw new NotImplementedException();
        }
    }
}
