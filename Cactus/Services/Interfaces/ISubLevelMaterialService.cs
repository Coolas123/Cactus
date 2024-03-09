using Cactus.Models.Database;
using Cactus.Models.Responses;

namespace Cactus.Services.Interfaces
{
    public interface ISubLevelMaterialService
    {
        Task<BaseResponse<string>> UpdateCoverAsync(IFormFile file, int id);
        Task<BaseResponse<SubLevelMaterial>> GetCover(int id);
    }
}
