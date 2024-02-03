using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Security.Claims;

namespace Cactus.Services.Interfaces
{
    public interface IIndividualService
    {
        Task<BaseResponse<ClaimsIdentity>> RegisterIndividual(SettingViewModel model, int id);
        Task<BaseResponse<Individual>> GetBuyUrlPage(string urlPage);
        Task<BaseResponse<Individual>> GetAsync(int id);
        Task<BaseResponse<Individual>> DaeleteIndividual(int id);
    }
}
