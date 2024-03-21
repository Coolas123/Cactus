using Cactus.Models.Responses;
using Cactus.Models.ViewModels;

namespace Cactus.Services.Interfaces
{
    public interface IPayMethodSettingService
    {
        Task<BaseResponse<bool>> AddSetting(PayMethodSettingViewModel model);
    }
}
