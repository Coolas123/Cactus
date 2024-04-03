using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;
using Cactus.Services.Interfaces;

namespace Cactus.Services.Implementations
{
    public class DonationOptionService : IDonationOptionService
    {
        private readonly IDonationOptionRepository donationOptionRepository;
        private readonly IPostDonationOptionService postDonationOptionService;
        public DonationOptionService(IDonationOptionRepository donationOptionRepository,
            IPostDonationOptionService postDonationOptionService) {
            this.donationOptionRepository = donationOptionRepository;
            this.postDonationOptionService = postDonationOptionService;
        }

        public async Task<BaseResponse<bool>> AddOptionAsync(NewDonationOption model) {
            DonationOption Option = new DonationOption
            {
                AuthorId = model.AuthorId,
                Description = model.Description,
                OptionName = model.OptionName,
                Price = model.Price,
                MonetizationTypeId = model.MonetizationTypeId
            };
            try {
                await donationOptionRepository.CreateAsync(Option);
            }
            catch{
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

        public async Task<BaseResponse<DonationOption>> GetByPriceAsync(decimal price) {
            DonationOption donationOption = await donationOptionRepository.GetByPriceAsync(price);
            if (donationOption == null) {
                return new BaseResponse<DonationOption>
                {
                    Description = "Опция не найдена"
                };
            }
            return new BaseResponse<DonationOption>
            {
                Data= donationOption,
                StatusCode = 200
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

        public async Task<BaseResponse<bool>> PayGoalAsync(int donationOptionId, decimal price) {
            DonationOption option = await donationOptionRepository.GetAsync(donationOptionId);
            option.Price += price;
            try {
                await donationOptionRepository.UpdateAsync(option);
            }
            catch {
                return new BaseResponse<bool>
                {
                    Description="Не удалось пополнить цель"
                };
            }
            return new BaseResponse<bool>
            {
                Data=true,
                StatusCode = 200
            };
        }
    }
}
