﻿using Cactus.Models.Database;
using Cactus.Models.Notifications;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;
using Cactus.Services.Implementations;
using Cactus.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Drawing;
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
        private readonly IPaidAuthorSubscribeService paidAuthorSubscribeService;
        private readonly IGoalService goalService;
        public MonetizationController(IDonationOptionService donationOptionService,
            ISubLevelMaterialService subLevelMaterialServices, ITransactionService transactionService,
            IWalletService walletService, IPayMethodSettingService payMethodSettingService,
            IDonatorService donatorService, IPaidAuthorSubscribeService paidAuthorSubscribeService,
            IGoalService goalService) {
            this.donationOptionService = donationOptionService;
            this.subLevelMaterialServices = subLevelMaterialServices;
            this.transactionService = transactionService;
            this.walletService = walletService;
            this.payMethodSettingService = payMethodSettingService;
            this.donatorService = donatorService;
            this.paidAuthorSubscribeService = paidAuthorSubscribeService;
            this.goalService = goalService;
        }

        [Authorize(Roles = "Author")]
        public IActionResult Index() {

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Author")]
        public async Task<IActionResult> AddSubLevel(NewDonationOptionViewModel model) {
            if (model.CoverFile != null) {
                var image = Image.FromStream(model.CoverFile.OpenReadStream());
                if (image.Width % image.Height !=0) {
                    ModelState.AddModelError("CoverFile", "Изображение должно имет ьсоотношение 1 к 1");
                    return View("Index", model);
                }
            }
            if (ModelState["Price"].Errors.Count != 0) {
                ModelState["Price"].Errors.Clear();
                ModelState.AddModelError("Price", "Неверная цена");
            }
            if (!ModelState.IsValid) {
                return View("Index", model);
            }
            await donationOptionService.AddOptionAsync(model);
            if (model.CoverFile != null) {
                BaseResponse<DonationOption> donationOption = await donationOptionService.GetByPriceAsync(model.Price);
                if (donationOption.StatusCode == 200) {
                    await subLevelMaterialServices.UpdateCoverAsync(model.CoverFile, donationOption.Data.Id);
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "Author")]
        public async Task<IActionResult> AddGoal(NewDonationOptionViewModel model) {
            if (ModelState["Price"].Errors.Count != 0) {
                ModelState["Price"].Errors.Clear();
                ModelState.AddModelError("Price", "Неверная цена");
            }
            if (!ModelState.IsValid) {
                return View("Index", model);
            }
            await goalService.CreateGoal(model);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "Author")]
        public async Task<IActionResult> AddRemittance(NewDonationOptionViewModel model) {
            if (ModelState["Price"].Errors.Count != 0) {
                ModelState["Price"].Errors.Clear();
                ModelState.AddModelError("Price", "Неверная цена");
            }
            if (!ModelState.IsValid) {
                return View("Index", model);
            }
            await donationOptionService.AddOptionAsync(model);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "Author, Patron")]
        public async Task<IActionResult> SubscribePaidSub(PagingAuthorViewModel model) {
            var paidSub = new NewPaidSubscribeViewModel();
            var authorNotifications = new AuthorNotifications();

            BaseResponse<IEnumerable<PaidAuthorSubscribe>> existedSubs = await paidAuthorSubscribeService.GetCurrentSubscribes(model.PaidSub.AuthorId, Convert.ToInt32(User.FindFirstValue("Id")));
            if (ModelState.GetValidationState(nameof(model.PaidSub)) == ModelValidationState.Unvalidated &&
                (existedSubs.StatusCode != 200 || 
                existedSubs.Data?.Where(x=>x.Donator.DonationOption.Price>= model.PaidSub.Sended).Count()==0)) {

                BaseResponse<PayMethodSetting> setting = await payMethodSettingService.GetIntrasystemOperationsSetting();
                model.PaidSub.Created = DateTime.Now;
                model.PaidSub.PayMethodId = setting.Data.Id;
                model.PaidSub.Received = model.PaidSub.Sended - model.PaidSub.Sended / 100 * setting.Data.Comission;
                model.PaidSub.StatusId = (int)Models.Enums.TransactionStatus.Sended;
                model.PaidSub.UserId = Convert.ToInt32(User.FindFirstValue("Id"));


                BaseResponse<Wallet> walletResponse = await walletService
                    .WithdrawWallet(Convert.ToInt32(User.FindFirstValue("Id")), model.PaidSub.Sended);

                if (walletResponse.StatusCode != 200) {
                    authorNotifications.EnoughBalance = "Недостаточно баланса";
                    return RedirectToAction("Index", "Author", new { id = model.PaidSub.AuthorId, authorNotifications.EnoughBalance });
                }
                await transactionService.CreateTransaction(model.PaidSub);
                await walletService.ReplenishWallet(model.PaidSub.AuthorId, model.PaidSub.Received);

                BaseResponse<Transaction> newTransaction = await transactionService
                    .GetLastTransaction(Convert.ToInt32(User.FindFirstValue("Id")), model.PaidSub.Created);

                var donatorViewModel = new DonatorViewModel
                {
                    UserId = model.PaidSub.UserId,
                    DonationOptionId = model.PaidSub.DonationOptionId,
                    DonationTargetTypeId = (int)Models.Enums.DonationTargetType.Author,
                    TransactionId = newTransaction.Data.Id
                };
                await donatorService.AddDonator(donatorViewModel);

                BaseResponse<Donator> lastDOnator = await donatorService.GetLastDonator(newTransaction.Data.Created, donatorViewModel.UserId);
                paidSub.StartDate = DateTime.Now;
                paidSub.EndDate = paidSub.StartDate.AddDays(30);
                paidSub.DonatorId = lastDOnator.Data.Id;

                await paidAuthorSubscribeService.SubscribeToAuthor(paidSub);
                authorNotifications.PaidSubscribed = "Поздравляем! вы оформили платную подписку";
            }
            return RedirectToAction("Index","Author",new { id=model.PaidSub.AuthorId, authorNotifications.PaidSubscribed });
        }

        [HttpPost]
        [Authorize(Roles = "Author, Patron")]
        public async Task<IActionResult> PayGoal(PagingAuthorViewModel model) {
            var authorNotifications = new AuthorNotifications();

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
                authorNotifications.EnoughBalance = "Недостаточно баланса";
                return RedirectToAction("Index", "Author", new { id = model.PayGoal.AuthorId, authorNotifications.EnoughBalance });
            }
            await transactionService.CreateTransaction(model.PayGoal);
            await walletService.ReplenishWallet(model.PayGoal.AuthorId, model.PayGoal.Received);

            BaseResponse<Transaction> newTransaction= await transactionService.GetLastTransaction(Convert.ToInt32(User.FindFirstValue("Id")), model.PayGoal.Created);
            var donatorViewModel = new DonatorViewModel
            {
                UserId = model.PayGoal.UserId,
                DonationOptionId = model.PayGoal.DonationOptionId,
                DonationTargetTypeId = (int)Models.Enums.DonationTargetType.Author,
                TransactionId = newTransaction.Data.Id
            };
            await donatorService.AddDonator(donatorViewModel);
            BaseResponse<Donator> lastDonator = await donatorService.GetLastDonator(newTransaction.Data.Created, model.PayGoal.UserId);
            await goalService.ReplenishGoal(model.PayGoal.DonationOptionId, newTransaction.Data.Sended);

            authorNotifications.PaidGoal = "Цель продвинулась!";
            return RedirectToAction("Index", "Author", new { id = model.PayGoal.AuthorId, authorNotifications.PaidGoal });
        }

        [HttpPost]
        [Authorize(Roles = "Author, Patron")]
        public async Task<IActionResult> Remittance(PagingAuthorViewModel model) {
            var authorNotifications = new AuthorNotifications();

            BaseResponse<PayMethodSetting> setting = await payMethodSettingService.GetIntrasystemOperationsSetting();
            model.Remittance.Created = DateTime.Now;
            model.Remittance.PayMethodId = setting.Data.Id;
            model.Remittance.Received = model.Remittance.Sended - model.Remittance.Sended / 100 * setting.Data.Comission;
            model.Remittance.StatusId = (int)Models.Enums.TransactionStatus.Sended;
            model.Remittance.UserId = Convert.ToInt32(User.FindFirstValue("Id"));

            BaseResponse<Wallet> walletResponse = await walletService.WithdrawWallet(Convert.ToInt32(User.FindFirstValue("Id")), model.Remittance.Sended);
            if (walletResponse.StatusCode != 200) {
                authorNotifications.EnoughBalance = "Недостаточно баланса";
                return RedirectToAction("Index", "Author", new { id = model.Remittance.AuthorId, authorNotifications.EnoughBalance });
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
            authorNotifications.Remittanced = "Донат отправлен";
            return RedirectToAction("Index", "Author", new { id = model.Remittance.AuthorId, authorNotifications.Remittanced });
        }

        public async Task<IActionResult> DoneGoal(int goalId, int authorId) {
            BaseResponse<bool> result = await goalService.DoneGoal(goalId);
            return RedirectToAction("Index","Author", new { id =authorId,GoalDone = result.Description});
        }
    }
}
