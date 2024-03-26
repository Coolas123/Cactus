using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;

namespace Cactus.Services.Interfaces
{
    public interface IPayMethodService
    {
        Task<BaseResponse<bool>> AddPayMethod(PayMethodViewModel model);
        Task<BaseResponse<PayMethod>> GetPayMethod(int id);
        Task<BaseResponse<IEnumerable<PayMethod>>> GetReplenishMethods();
    }
}
