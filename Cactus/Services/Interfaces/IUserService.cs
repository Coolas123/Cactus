using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;
using System.Security.Claims;

namespace Cactus.Services.Interfaces
{
    public interface IUserService
    {
        Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model);
        ClaimsIdentity Authenticate(User user, int id);
        Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model);
        Task<ModelErrorsResponse<ClaimsIdentity>> ChangeSettingsAsync(SettingViewModel model, int id);
        Task<BaseResponse<ClaimsIdentity>> ChangeRoleToAuthor(int id);
        Task<BaseResponse<User>> AddToCacheAsync(string email);
        Task<BaseResponse<User>> GetAsync(int id);
    }
}
