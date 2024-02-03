using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Security.Claims;

namespace Cactus.Services.Interfaces
{
    public interface IUserService
    {
        Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model);
        ClaimsIdentity Authenticate(User user, int id);
        Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model);
        Task<ModelErrorsResponse<ClaimsIdentity>> ChangeSettingsAsync(SettingViewModel model, int id);
        Task<BaseResponse<ClaimsIdentity>> ChangeRoleToIndividual(int id);
        Task<BaseResponse<User>> AddToCacheAsync(string email);
        Task<BaseResponse<User>> GetUserAsync(int id);
        Task<BaseResponse<ClaimsIdentity>> ChangeRoleToLegal(int id);
    }
}
