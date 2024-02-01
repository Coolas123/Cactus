using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Cactus.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlTypes;

namespace Cactus.Infrastructure.Repositories
{
    public class ProfileMaterialRepository : IProfileMaterialRepository
    {
        private readonly ApplicationDbContext dbContext;
        public ProfileMaterialRepository(ApplicationDbContext dbContext) {
            this.dbContext = dbContext;
        }

        public async Task<bool> CreateAsync(ProfileMaterial entity) {
            await dbContext.ProfileMaterials.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(ProfileMaterial entity) {
            dbContext.ProfileMaterials.Remove(entity);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<ProfileMaterial> GetAsync(int id) {
            return await dbContext.ProfileMaterials.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> UpdateAvatarAsync(ProfileMaterial entity) {
            ProfileMaterial avatar = dbContext.ProfileMaterials.SingleOrDefault(x => x.UserId == entity.UserId && x.MaterialTypeId == (int)Models.Enums.MaterialType.Avatar);
            if (avatar != null) {
                avatar.Title = entity.Title;
                avatar.Path = entity.Path;
                dbContext.ProfileMaterials.Update(avatar);
            }
            else {
                await dbContext.ProfileMaterials.AddAsync(entity);
            }
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ProfileMaterial>> SelectAsync() {
            return await dbContext.ProfileMaterials.ToListAsync();
        }

        public async Task<ProfileMaterial> GetAvatarAsync(int userId) {
            return await dbContext.ProfileMaterials.FirstOrDefaultAsync(x => x.UserId == userId && x.MaterialTypeId == (int)Models.Enums.MaterialType.Avatar);
        }

        public async Task<ProfileMaterial> GetBannerAsync(int userId) {
            return await dbContext.ProfileMaterials.FirstOrDefaultAsync(x => x.UserId == userId && x.MaterialTypeId == (int)Models.Enums.MaterialType.Banner);
        }

        public async Task<bool> UpdateBannerAsync(ProfileMaterial entity) {
            ProfileMaterial banner = dbContext.ProfileMaterials.SingleOrDefault(x => x.UserId == entity.UserId && x.MaterialTypeId == (int)Models.Enums.MaterialType.Banner);
            if (banner != null) {
                banner.Title = entity.Title;
                banner.Path = entity.Path;
                dbContext.ProfileMaterials.Update(banner);
                await dbContext.SaveChangesAsync();
            }
            else {
                await dbContext.ProfileMaterials.AddAsync(entity);
            }
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<ProfileMaterial> GetPostPhotoAsync(int userId) {
            return await dbContext.ProfileMaterials.FirstOrDefaultAsync(x => x.UserId == userId && x.MaterialTypeId == (int)Models.Enums.MaterialType.PostPhoto);
        }

        public async Task<IEnumerable<ProfileMaterial>> GetProfileMaterialAsync(int userId) {
            return await dbContext.ProfileMaterials.Include(x=>x.User).Where(x=>x.UserId==userId).ToListAsync();
        }
    }
}
