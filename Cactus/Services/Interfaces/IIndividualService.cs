using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;
using System.Security.Claims;

namespace Cactus.Services.Interfaces
{
    public interface IIndividualService
    {
        Task<BaseResponse<Individual>> RegisterIndividual(RegisterIndividualViewModel model, int id);
    }
}
