﻿using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;
using Cactus.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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

        [Authorize(Roles = "Author")]
        public IActionResult Index() {

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Author")]
        public async Task<IActionResult> AddSubLevel(MonetizationViewModel model) {
            await donationOptionService.AddOptionAsync(model.NewDonationOption);
            if (model.NewDonationOption.CoverFile != null) {
                BaseResponse<DonationOption> donationOption = await donationOptionService.GetByPriceAsync(model.NewDonationOption.Price);
                if (donationOption.StatusCode == 200) {
                    await subLevelMaterialServices.UpdateCoverAsync(model.NewDonationOption.CoverFile, donationOption.Data.Id);
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "Author")]
        public async Task<IActionResult> AddGoal(MonetizationViewModel model) {
            await donationOptionService.AddOptionAsync(model.NewDonationOption);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "Author")]
        public async Task<IActionResult> AddRemittance(MonetizationViewModel model) {
            await donationOptionService.AddOptionAsync(model.NewDonationOption);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "Author, Patron")]
        public async Task<IActionResult> SubscribePaidSub(PagingAuthorViewModel model) {
            if (ModelState.GetValidationState(nameof(model.PaidSub)) == ModelValidationState.Unvalidated) {
                BaseResponse<PayMethodSetting> setting = await payMethodSettingService.GetIntrasystemOperationsSetting();
                model.PaidSub.Created = DateTime.Now;
                model.PaidSub.PayMethodId = setting.Data.Id;
                model.PaidSub.Received = model.PaidSub.Sended - model.PaidSub.Sended / 100 * setting.Data.Comission;
                model.PaidSub.StatusId = (int)Models.Enums.TransactionStatus.Sended;
                model.PaidSub.UserId = Convert.ToInt32(User.FindFirstValue("Id"));

                BaseResponse<Wallet> walletResponse = await walletService.WithdrawWallet(Convert.ToInt32(User.FindFirstValue("Id")), model.PaidSub.Sended);
                if (walletResponse.StatusCode != 200) {
                    return RedirectToAction("Index", "Author", new { id = model.PaidSub.AuthorId, NotEnoughBalance = true });
                }
                await transactionService.CreateTransaction(model.PaidSub);
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
            }
            return RedirectToAction("Index","Author",new { id=model.PaidSub.AuthorId});
        }

        [HttpPost]
        [Authorize(Roles = "Author, Patron")]
        public async Task<IActionResult> PayGoal(PagingAuthorViewModel model) {
            foreach (var state in ModelState) {
                if (state.Key.Split('.').Contains("PayGoal") &&
                    state.Value.Errors.Count != 0) {
                    return RedirectToAction("Index","Author", new { UrlPage = "",id = model .PayGoal.AuthorId});
                }
            }
            BaseResponse<PayMethodSetting> setting = await payMethodSettingService.GetIntrasystemOperationsSetting();
            model.PayGoal.Created = DateTime.Now;
            model.PayGoal.PayMethodId = setting.Data.Id;
            model.PayGoal.Received = model.PayGoal.Sended - model.PayGoal.Sended / 100 * setting.Data.Comission;
            model.PayGoal.StatusId = (int)Models.Enums.TransactionStatus.Sended;
            model.PayGoal.UserId = Convert.ToInt32(User.FindFirstValue("Id"));

            BaseResponse<Wallet> walletResponse = await walletService.WithdrawWallet(Convert.ToInt32(User.FindFirstValue("Id")), model.PayGoal.Sended);
            if (walletResponse.StatusCode != 200) {
                return RedirectToAction("Index", "Author", new { id = model.PayGoal.AuthorId, NotEnoughBalance = true });
            }
            await transactionService.CreateTransaction(model.PayGoal);
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

            BaseResponse<Wallet> walletResponse = await walletService.WithdrawWallet(Convert.ToInt32(User.FindFirstValue("Id")), model.Remittance.Sended);
            if (walletResponse.StatusCode != 200) {
                return RedirectToAction("Index", "Author", new { id = model.Remittance.AuthorId, NotEnoughBalance = true });
            }
            await transactionService.CreateTransaction(model.Remittance);
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
