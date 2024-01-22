using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlTypes;

namespace Cactus.Infrastructure.Repositories
{
    public class MaterialRepository : IMaterialRepository
    {
        private readonly ApplicationDbContext dbContext;
        public MaterialRepository(ApplicationDbContext dbContext) {
            this.dbContext = dbContext;
        }

        public async Task<bool> CreateAsync(Material entity) {
            await dbContext.Materials.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Material entity) {
            dbContext.Materials.Remove(entity);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Material> GetAsync(int id) {
            return await dbContext.Materials.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> UpdateAvatarAsync(Material entity) {
            Material avatar = dbContext.Materials.SingleOrDefault(x => x.UserId == entity.UserId && x.MaterialTypeId == (int)Models.Enums.MaterialType.Avatar);
            if (avatar != null) {
                avatar.Title = entity.Title;
                avatar.Path = entity.Path;
                dbContext.Materials.Update(avatar);
            }
            else {
                await dbContext.Materials.AddAsync(entity);
            }
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Material>> SelectAsync() {
            return await dbContext.Materials.ToListAsync();
        }

        public async Task<Material> GetAvatarAsync(int userId) {
            return await dbContext.Materials.FirstOrDefaultAsync(x => x.UserId == userId && x.MaterialTypeId == (int)Models.Enums.MaterialType.Avatar);
        }

        public async Task<Material> GetBannerAsync(int userId) {
            return await dbContext.Materials.FirstOrDefaultAsync(x => x.UserId == userId && x.MaterialTypeId == (int)Models.Enums.MaterialType.Banner);
        }

        public async Task<bool> UpdateBannerAsync(Material entity) {
            Material banner = dbContext.Materials.SingleOrDefault(x => x.UserId == entity.UserId && x.MaterialTypeId == (int)Models.Enums.MaterialType.Banner);
            if (banner != null) {
                banner.Title = entity.Title;
                banner.Path = entity.Path;
                dbContext.Materials.Update(banner);
                await dbContext.SaveChangesAsync();
            }
            else {
                await dbContext.Materials.AddAsync(entity);
            }
            await dbContext.SaveChangesAsync();
            return true;
        }
    }
}
