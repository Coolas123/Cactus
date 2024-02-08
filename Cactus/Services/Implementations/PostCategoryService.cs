using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Services.Interfaces;

namespace Cactus.Services.Implementations
{
    public class PostCategoryService : IPostCategoryService
    {
        private readonly IPostCategoryRepository postCategoryRepository;
        public PostCategoryService(IPostCategoryRepository postCategoryRepository) {
            this.postCategoryRepository = postCategoryRepository;
        }
        public async Task<BaseResponse<bool>> CreateAsync(int postId, int categoryId) {
            var postCategory = new PostCategory
            {
                PostId = postId,
                CategoryId = categoryId
            };
            bool result = await postCategoryRepository.CreateAsync(postCategory);
            if (!result) {
                return new BaseResponse<bool>
                {
                    Description="Не удалось добавить категорию"
                };
            }
            return new BaseResponse<bool>
            {
                Data = true,
                StatusCode = 200
            };
        }
    }
}
