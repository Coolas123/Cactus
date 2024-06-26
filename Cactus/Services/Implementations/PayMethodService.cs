﻿using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;
using Cactus.Services.Interfaces;

namespace Cactus.Services.Implementations
{
    public class PayMethodService: IPayMethodService
    {
        private readonly IPayMethodRepository payMethodRepository;
        public PayMethodService(IPayMethodRepository payMethodRepository) {
            this.payMethodRepository = payMethodRepository;
        }

        public async Task<BaseResponse<bool>> AddPayMethod(PayMethodViewModel model) {
            var method = new PayMethod
            {
                Name = model.Name,
                PayMethodSettingId = model.PayMethodSettingId,
            };
            try {
                await payMethodRepository.CreateAsync(method);
            }
            catch {
                return new BaseResponse<bool>
                {
                    Description = "Не удалось добавить метод"
                };
            }
            return new BaseResponse<bool>
            {
                Description = $"Метод {model.Name} добавлен",
                Data = true,
                StatusCode = 200
            };
        }

        public async Task<BaseResponse<PayMethod>> GetPayMethod(int id) {
            PayMethod method = await payMethodRepository.GetAsync(id);
            if (method == null) {
                return new BaseResponse<PayMethod>
                {
                    Description = "Метод не найден"
                };
            }
            return new BaseResponse<PayMethod>
            {
                Data = method,
                StatusCode = 200
            };
        }

        public async Task<BaseResponse<IEnumerable<PayMethod>>> GetMethods() {
            IEnumerable<PayMethod> methods = await payMethodRepository.GetMethodsAsync();
            if (!methods.Any()) {
                return new BaseResponse<IEnumerable<PayMethod>>
                {
                    Description="Методы отсутствуют"
                };
            }
            return new BaseResponse<IEnumerable<PayMethod>>
            {
                Data= methods,
                StatusCode = 200
            };
        }
    }
}
