using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Security.Claims;

namespace Cactus.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<BaseResponse<ClaimsIdentity>> RegisterAuthor(SettingViewModel model, int id);
        Task<BaseResponse<Author>> GetBuyUrlPage(string urlPage);
        Task<BaseResponse<Author>> GetAsync(int id);
        Task<BaseResponse<User>> GetUserAsync(int id);
        Task<BaseResponse<Author>> DaeleteAuthor(int id);
        Task<BaseResponse<User>> GetUserByUrlPageAsync(string urlPage);
        Task<BaseResponse<Author>> GetUserByNameAsync(IEnumerable<string> names);
    }
}
