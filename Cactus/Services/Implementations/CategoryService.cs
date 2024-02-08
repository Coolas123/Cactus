using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Services.Interfaces;

namespace Cactus.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository) {
            this.categoryRepository = categoryRepository;
        }
        public async Task<BaseResponse<IEnumerable<Category>>> GetAll() {
            IEnumerable<Category> categories = await categoryRepository.SelectAsync();
            return new BaseResponse<IEnumerable<Category>> { Data = categories, StatusCode = 200 };
        }
    }
}
