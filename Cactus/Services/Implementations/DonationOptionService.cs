﻿using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;
using Cactus.Services.Interfaces;

namespace Cactus.Services.Implementations
{
    public class DonationOptionService : IDonationOptionService
    {
        private readonly IDonationOptionRepository donationOptionRepository;
        public DonationOptionService(IDonationOptionRepository donationOptionRepository) {
            this.donationOptionRepository = donationOptionRepository;
        }

        public async Task<BaseResponse<bool>> AddOptionAsync(NewDonationOption model) {
            DonationOption Option = new DonationOption
            {
                AuthorId = model.AuthorId,
                Description = model.Description,
                OptionName = model.OptionName,
                MinPrice = model.MinPrice,
                MaxPrice = model.MinPrice>model.MaxPrice? model.MinPrice: model.MaxPrice,
                MonetizationTypeId = model.MonetizationTypeId
            };
            try {
                await donationOptionRepository.CreateAsync(Option);
            }
            catch (Exception ex) {
                return new BaseResponse<bool>
                {
                    Description="Не удалось создать опцию"
                };
            }
            return new BaseResponse<bool>
            {
                Data= true,
                StatusCode=200
            };
        }

        public async Task<BaseResponse<IEnumerable<DonationOption>>> GetOptionsAsync(int authorId) {
            IEnumerable<DonationOption> options =await donationOptionRepository.GetOptionsAsync(authorId);
            if (!options.Any()) {
                return new BaseResponse<IEnumerable<DonationOption>> { 
                    Description="Опции не найдены"
                };
            }
            return new BaseResponse<IEnumerable<DonationOption>>
            {
                Data = options,
                StatusCode = 200
            };
        }
    }
}
