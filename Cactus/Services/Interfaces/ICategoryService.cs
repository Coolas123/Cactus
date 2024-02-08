using Cactus.Models.Database;
using Cactus.Models.Responses;

namespace Cactus.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<BaseResponse<IEnumerable<Category>>> GetAll();
    }
}
