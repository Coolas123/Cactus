using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;
using Cactus.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cactus.Controllers
{
    [Authorize(Roles = "Author")]
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
                BaseResponse<DonationOption> donationOption = await donationOptionService.GetByPriceAsync(model.NewSubLevelDonationOption.NewDonationOption.Price);
                if (donationOption.StatusCode == 200) {
                    await subLevelMaterialServices.UpdateCoverAsync(model.NewSubLevelDonationOption.CoverFile, donationOption.Data.Id);
                }
            }
            return RedirectToAction("Index","Setting");
        }

        [HttpPost]
        public async Task<IActionResult> AddGoal(SettingViewModel model) {
            await donationOptionService.AddOptionAsync(model.NewSubLevelDonationOption.NewDonationOption);
            return RedirectToAction("Index", "Setting");
        }

        [HttpPost]
        public async Task<IActionResult> AddRemittance(SettingViewModel model) {
            await donationOptionService.AddOptionAsync(model.NewSubLevelDonationOption.NewDonationOption);
            return RedirectToAction("Index", "Setting");
        }
    }
}
