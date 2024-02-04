using Cactus.Models.Database;
using Cactus.Models.Responses;

namespace Cactus.Services.Interfaces
{
    public interface IPatronService
    {
        Task<BaseResponse<Patron>> DaeleteUser(int id);
        Task<BaseResponse<Patron>> GetAsync(int id);
    }
}
