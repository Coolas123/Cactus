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
    }
}
