using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;
using System.Security.Claims;

namespace Cactus.Services.Interfaces
{
    public interface ILegalService
    {
        Task<BaseResponse<ClaimsIdentity>> RegisterLegal(RegisterLegalViewModel model, int id);
        Task<BaseResponse<Legal>> GetAsync(int id);
        Task<BaseResponse<User>> GetUserByUrlPageAsync(string urlPage);
    }
}
