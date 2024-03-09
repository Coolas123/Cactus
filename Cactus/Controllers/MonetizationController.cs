using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;
using Cactus.Services.Implementations;
using Cactus.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace Cactus.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class MonetizationController:Controller
    {
        private readonly IDonationOptionService donationOptionService;
        private readonly ISubLevelMaterialService subLevelMaterialServices;
        public MonetizationController(IDonationOptionService donationOptionService,
            ISubLevelMaterialService subLevelMaterialServices) {
            this.donationOptionService = donationOptionService;
            this.subLevelMaterialServices = subLevelMaterialServices;
        }

        [HttpPost]
        public async Task<IActionResult> AddSubLevel(SettingViewModel model) {
            await donationOptionService.AddOptionAsync(model.NewSubLevelDonationOption.NewDonationOption);
            if (model.NewSubLevelDonationOption.CoverFile != null) {
                BaseResponse<DonationOption> donationOption = await donationOptionService.GetByPriceAsync(model.NewSubLevelDonationOption.NewDonationOption.MinPrice);
                if (donationOption.StatusCode == 200) {
                    await subLevelMaterialServices.UpdateCoverAsync(model.NewSubLevelDonationOption.CoverFile, donationOption.Data.Id);
                }
            }
            return Redirect("/");
        }
    }
}
