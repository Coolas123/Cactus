using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;
using Cactus.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Cactus.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class MonetizationController:Controller
    {
        private readonly IDonationOptionService donationOptionService;
        private readonly ISubLevelMaterialService subLevelMaterialServices;
        private readonly ITransactionService transactionService;
        private readonly IWalletService walletService;
        private readonly IPayMethodSettingService payMethodSettingService;
        public MonetizationController(IDonationOptionService donationOptionService,
            ISubLevelMaterialService subLevelMaterialServices, ITransactionService transactionService,
            IWalletService walletService, IPayMethodSettingService payMethodSettingService) {
            this.donationOptionService = donationOptionService;
            this.subLevelMaterialServices = subLevelMaterialServices;
            this.transactionService = transactionService;
            this.walletService = walletService;
            this.payMethodSettingService = payMethodSettingService;
        }

        [HttpPost]
        [Authorize(Roles = "Author")]
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
        [Authorize(Roles = "Author")]
        public async Task<IActionResult> AddGoal(SettingViewModel model) {
            await donationOptionService.AddOptionAsync(model.NewSubLevelDonationOption.NewDonationOption);
            return RedirectToAction("Index", "Setting");
        }

        [HttpPost]
        [Authorize(Roles = "Author")]
        public async Task<IActionResult> AddRemittance(SettingViewModel model) {
            await donationOptionService.AddOptionAsync(model.NewSubLevelDonationOption.NewDonationOption);
            return RedirectToAction("Index", "Setting");
        }

        [HttpPost]
        [Authorize(Roles = "Patron")]
        public async Task<IActionResult> SubscribePaidSub(PagingAuthorViewModel model) {
            BaseResponse<PayMethodSetting> setting = await payMethodSettingService.GetIntrasystemOperationsSetting();
            model.PaidSub.Created = DateTime.Now;
            model.PaidSub.PayMethodId = setting.Data.Id;
            model.PaidSub.Received = model.PaidSub.Sended - model.PaidSub.Sended / 100 * setting.Data.Comission;
            model.PaidSub.StatusId = (int)Models.Enums.TransactionStatus.Sended;
            model.PaidSub.UserId = Convert.ToInt32(User.FindFirstValue("Id"));

            await transactionService.CreateTransaction(model.PaidSub);
            await walletService.WithdrawWallet(Convert.ToInt32(User.FindFirstValue("Id")), model.PaidSub.Sended);
            await walletService.ReplenishWallet(model.PaidSub.AuthorId, model.PaidSub.Received);
            return RedirectToAction("Index","Author",new { id=model.PaidSub.AuthorId});
        }
    }
}
