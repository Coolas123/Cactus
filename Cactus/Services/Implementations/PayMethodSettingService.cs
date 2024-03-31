using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;
using Cactus.Services.Interfaces;

namespace Cactus.Services.Implementations
{
    public class PayMethodSettingService : IPayMethodSettingService
    {
        private readonly IPayMethodSettingRepository payMethodSettingRepository;
        public PayMethodSettingService(IPayMethodSettingRepository payMethodSettingRepository) {
            this.payMethodSettingRepository = payMethodSettingRepository;
        }
        public async Task<BaseResponse<bool>> AddSetting(PayMethodSettingViewModel model) {
            var setting = new PayMethodSetting
            {
                Comission = model.Comission,
                DailyWithdrawLimit = model.DailyWithdrawLimit,
                MonthlyWithdrawLimit = model.MonthlyWithdrawLimit,
            };
            try {
                await payMethodSettingRepository.CreateAsync(setting);
            }
            catch {
                return new BaseResponse<bool>
                {
                    Description="Не удалось добавить настройку"
                };
            }
            return new BaseResponse<bool>
            {
                Description = "Настройка добавлена",
                Data=true,
                StatusCode=200
            };
        }

        public async Task<BaseResponse<PayMethodSetting>> GetIntrasystemOperationsSetting() {
            PayMethodSetting setting = await payMethodSettingRepository.GetIntrasystemOperationsSettingAsync();
            return new BaseResponse<PayMethodSetting>
            {
                Data= setting,
                StatusCode = 200
            };
        }

        public async Task<BaseResponse<PayMethodSetting>> GetSettingAsync(int id) {
            PayMethodSetting setting = await payMethodSettingRepository.GetAsync(id);
            if (setting == null) {
                return new BaseResponse<PayMethodSetting>
                {
                    Description = "Настрйока не найдена"
                };
            }
            return new BaseResponse<PayMethodSetting>
            {
                Data=setting,
                StatusCode=200
            };
        }
    }
}
