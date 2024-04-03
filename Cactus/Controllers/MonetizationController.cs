using Cactus.Models.Database;
using Cactus.Models.Enums;
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
        private readonly IDonatorService donatorService;
        public MonetizationController(IDonationOptionService donationOptionService,
            ISubLevelMaterialService subLevelMaterialServices, ITransactionService transactionService,
            IWalletService walletService, IPayMethodSettingService payMethodSettingService,
            IDonatorService donatorService) {
            this.donationOptionService = donationOptionService;
            this.subLevelMaterialServices = subLevelMaterialServices;
            this.transactionService = transactionService;
            this.walletService = walletService;
            this.payMethodSettingService = payMethodSettingService;
            this.donatorService = donatorService;
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
        [Authorize(Roles = "Author, Patron")]
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

            BaseResponse<Transaction> newTransaction = await transactionService.GetLastTransaction(Convert.ToInt32(User.FindFirstValue("Id")), model.PaidSub.Created);
            var donatorViewModel = new DonatorViewModel
            {
                UserId = model.PaidSub.UserId,
                DonationOptionId = model.PaidSub.DonationOptionId,
                DonationTargetTypeId = (int)Models.Enums.DonationTargetType.Author,
                TransactionId = newTransaction.Data.Id
            };
            await donatorService.AddDonator(donatorViewModel);
            return RedirectToAction("Index","Author",new { id=model.PaidSub.AuthorId});
        }

        [HttpPost]
        [Authorize(Roles = "Author, Patron")]
        public async Task<IActionResult> PayGoal(PagingAuthorViewModel model) {
            BaseResponse<PayMethodSetting> setting = await payMethodSettingService.GetIntrasystemOperationsSetting();
            model.PayGoal.Created = DateTime.Now;
            model.PayGoal.PayMethodId = setting.Data.Id;
            model.PayGoal.Received = model.PayGoal.Sended - model.PayGoal.Sended / 100 * setting.Data.Comission;
            model.PayGoal.StatusId = (int)Models.Enums.TransactionStatus.Sended;
            model.PayGoal.UserId = Convert.ToInt32(User.FindFirstValue("Id"));

            await transactionService.CreateTransaction(model.PayGoal);
            await walletService.WithdrawWallet(Convert.ToInt32(User.FindFirstValue("Id")), model.PayGoal.Sended);
            await walletService.ReplenishWallet(model.PayGoal.AuthorId, model.PayGoal.Received);
            await donationOptionService.PayGoalAsync(model.PayGoal.DonationOptionId, model.PayGoal.Received);

            BaseResponse<Transaction> newTransaction= await transactionService.GetLastTransaction(Convert.ToInt32(User.FindFirstValue("Id")), model.PayGoal.Created);
            var donatorViewModel = new DonatorViewModel
            {
                UserId = model.PayGoal.UserId,
                DonationOptionId = model.PayGoal.DonationOptionId,
                DonationTargetTypeId = (int)Models.Enums.DonationTargetType.Author,
                TransactionId = newTransaction.Data.Id
            };
            await donatorService.AddDonator(donatorViewModel);
            return RedirectToAction("Index", "Author", new { id = model.PayGoal.AuthorId });
        }

        [HttpPost]
        [Authorize(Roles = "Author, Patron")]
        public async Task<IActionResult> Remittance(PagingAuthorViewModel model) {
            BaseResponse<PayMethodSetting> setting = await payMethodSettingService.GetIntrasystemOperationsSetting();
            model.Remittance.Created = DateTime.Now;
            model.Remittance.PayMethodId = setting.Data.Id;
            model.Remittance.Received = model.Remittance.Sended - model.Remittance.Sended / 100 * setting.Data.Comission;
            model.Remittance.StatusId = (int)Models.Enums.TransactionStatus.Sended;
            model.Remittance.UserId = Convert.ToInt32(User.FindFirstValue("Id"));

            await transactionService.CreateTransaction(model.Remittance);
            await walletService.WithdrawWallet(Convert.ToInt32(User.FindFirstValue("Id")), model.Remittance.Sended);
            await walletService.ReplenishWallet(model.Remittance.AuthorId, model.Remittance.Received);

            BaseResponse<Transaction> newTransaction = await transactionService.GetLastTransaction(Convert.ToInt32(User.FindFirstValue("Id")), model.Remittance.Created);
            var donatorViewModel = new DonatorViewModel
            {
                UserId = model.Remittance.UserId,
                DonationOptionId = model.Remittance.DonationOptionId,
                DonationTargetTypeId = (int)Models.Enums.DonationTargetType.Author,
                TransactionId = newTransaction.Data.Id
            };
            await donatorService.AddDonator(donatorViewModel);
            return RedirectToAction("Index", "Author", new { id = model.Remittance.AuthorId });
        }
    }
}
