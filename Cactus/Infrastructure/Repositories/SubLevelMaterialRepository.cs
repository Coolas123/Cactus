using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace Cactus.Infrastructure.Repositories
{
    public class SubLevelMaterialRepository : ISubLevelMaterialRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SubLevelMaterialRepository(ApplicationDbContext dbContext) {
            this.dbContext = dbContext;
        }

        public Task<bool> CreateAsync(SubLevelMaterial entity) {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(SubLevelMaterial entity) {
            throw new NotImplementedException();
        }

        public async Task<SubLevelMaterial> GetAsync(int id) {
            return await dbContext.SubLevelMaterials.FirstOrDefaultAsync(x=>x.Id==id);
        }

        public async Task<IEnumerable<SubLevelMaterial>> GetMaterialsAsync(int authorId) {
            return await dbContext.SubLevelMaterials.Include(x => x.DonationOption).Where(x => x.DonationOption.AuthorId == authorId).ToListAsync();
        }

        public Task<IEnumerable<SubLevelMaterial>> SelectAsync() {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateCoverAsync(SubLevelMaterial entity) {
            SubLevelMaterial cover = dbContext.SubLevelMaterials.SingleOrDefault(x => x.DonationOptionId == entity.DonationOptionId && x.MaterialTypeId == (int)Models.Enums.MaterialType.SubLevelCover);
            if (cover != null) {
                cover.Title = entity.Title;
                cover.Path = entity.Path;
                dbContext.SubLevelMaterials.Update(cover);
            }
            else {
                await dbContext.SubLevelMaterials.AddAsync(entity);
            }
            await dbContext.SaveChangesAsync();
            return true;
        }
    }
}
