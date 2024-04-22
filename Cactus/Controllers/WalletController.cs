using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;
using Cactus.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nest;
using System.Security.Claims;

namespace Cactus.Controllers
{
    [Authorize(Roles = "Patron, Author")]
    [AutoValidateAntiforgeryToken]
    public class WalletController: Controller
    {
        private readonly ITransactionService transactionService;
        private readonly IWalletService walletService;
        private readonly IDonatorService donatorService;
        private readonly IPayMethodService payMethodService;
        public WalletController(ITransactionService transactionService, IWalletService walletService,
            IDonatorService donatorService, IPayMethodService payMethodService) {
            this.transactionService = transactionService;
            this.walletService = walletService;
            this.donatorService = donatorService;
            this.payMethodService = payMethodService;
        }

        public async Task<IActionResult> Index() {
            var walletSettingViewModel = new WalletSettingViewModel();
            BaseResponse<IEnumerable<PayMethod>> methods = await payMethodService.GetMethods();
            if (methods.StatusCode == 200) {
                walletSettingViewModel.PayMethods = methods.Data;
            }

            BaseResponse<Wallet> wallet = await walletService.GetWallet(Convert.ToInt32(User.FindFirstValue("Id")));
            walletSettingViewModel.Wallet = wallet.Data;
            return View(walletSettingViewModel);
        }
        public async Task<IActionResult> DonationHistory() {
            BaseResponse<IEnumerable<Donator>> donators = await donatorService.GetDonators(Convert.ToInt32(User.FindFirstValue("Id")));
            return View(donators);
        }

        [HttpPost]
        public async Task<IActionResult> ReplenishWallet(MonetizationViewModel model) {
            model.Replenish.Created = DateTime.Now;
            model.Replenish.Received = model.Replenish.Sended-model.Replenish.Sended/100*model.Replenish.Comission;
            model.Replenish.StatusId = (int)Models.Enums.TransactionStatus.Sended;
            model.Replenish.UserId = Convert.ToInt32(User.FindFirstValue("Id"));

            await transactionService.CreateTransaction(model.Replenish);
            await walletService.ReplenishWallet(Convert.ToInt32(User.FindFirstValue("Id")), model.Replenish.Received);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<JsonResult> SelectPaySetting(int payMethodId) {
            BaseResponse<PayMethod> method = await payMethodService.GetPayMethod(payMethodId);
            return new JsonResult(method.Data.PayMethodSetting.Comission);
        }

        public async Task<IActionResult> History() {
            BaseResponse<IEnumerable<Transaction>> transactions = 
                await transactionService
                .GetWidrawAndReplenishAsync(Convert.ToInt32(User.FindFirstValue("Id")));
            return View(transactions);
        }

        [HttpPost]
        public async Task<IActionResult> WithdrawWallet(MonetizationViewModel model) {
            model.Withdraw.Created = DateTime.Now;
            model.Withdraw.Received = model.Withdraw.Sended - model.Withdraw.Sended / 100 * model.Withdraw.Comission;
            model.Withdraw.StatusId = (int)Models.Enums.TransactionStatus.Sended;
            model.Withdraw.UserId = Convert.ToInt32(User.FindFirstValue("Id"));

            await transactionService.CreateTransaction(model.Withdraw);
            await walletService.WithdrawWallet(Convert.ToInt32(User.FindFirstValue("Id")), model.Withdraw.Sended);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> HistoryFilter(DateTime dateFrom, DateTime dateTo) {
            BaseResponse<IEnumerable<Transaction>> transactions =
                await transactionService
                .GetWidrawAndReplenishAsync(Convert.ToInt32(User.FindFirstValue("Id")), dateFrom.ToUniversalTime(), dateTo.ToUniversalTime());
            return View("History",transactions);
        }

        [HttpPost]
        public async Task<IActionResult> IndexFilter(DateTime dateFrom, DateTime dateTo) {
            BaseResponse<IEnumerable<Donator>> donators = 
                await donatorService.GetDonators(Convert.ToInt32(User.FindFirstValue("Id")), dateFrom, dateTo);
            return View("DonationHistory", donators);
        }
    }
}
