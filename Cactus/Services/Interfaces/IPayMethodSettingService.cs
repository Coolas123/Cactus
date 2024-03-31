using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;

namespace Cactus.Services.Interfaces
{
    public interface IPayMethodSettingService
    {
        Task<BaseResponse<bool>> AddSetting(PayMethodSettingViewModel model);
        Task<BaseResponse<PayMethodSetting>> GetSettingAsync(int id);
        Task<BaseResponse<PayMethodSetting>> GetIntrasystemOperationsSetting();
    }
}
