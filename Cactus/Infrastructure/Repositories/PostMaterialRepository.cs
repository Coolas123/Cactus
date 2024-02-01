using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace Cactus.Infrastructure.Repositories
{
    public class PostMaterialRepository : IPostMaterialRepository
    {
        private readonly ApplicationDbContext dbContext;
        public PostMaterialRepository(ApplicationDbContext dbContext) {
            this.dbContext = dbContext;
        }

        public async Task<bool> CreateAsync(PostMaterial entity) {
            await dbContext.PostMaterials.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public Task<bool> DeleteAsync(PostMaterial entity) {
            throw new NotImplementedException();
        }

        public async Task<PostMaterial> GetAsync(int id) {
            return await dbContext.PostMaterials.FirstOrDefaultAsync(x=>x.PostId==id);
        }

        public async Task<PostMaterial> GetPhotoAsync(int postId) {
            return await dbContext.PostMaterials.FirstOrDefaultAsync(x=>x.MaterialTypeId==(int)Models.Enums.MaterialType.PostPhoto);
        }

        public Task<IEnumerable<PostMaterial>> SelectAsync() {
            throw new NotImplementedException();
        }
    }
}
