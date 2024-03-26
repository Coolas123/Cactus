using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;
using Cactus.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Cactus.Controllers
{
    [Authorize(Roles = "Author")]
    public class WalletController: Controller
    {
        private readonly ITransactionService transactionService;
        private readonly IWalletService walletService;
        private readonly IDonatorService donatorService;
        private readonly IAuthorService authorService;
        private readonly IPayMethodSettingService payMethodSettingService;
        private readonly IPayMethodService payMethodService;
        public WalletController(ITransactionService transactionService, IWalletService walletService,
            IDonatorService donatorService, IAuthorService authorService,
            IPayMethodSettingService payMethodSettingService, IPayMethodService payMethodService) {
            this.transactionService = transactionService;
            this.walletService = walletService;
            this.donatorService = donatorService;
            this.authorService = authorService;
            this.payMethodSettingService = payMethodSettingService;
            this.payMethodService = payMethodService;
        }
        public async Task<IActionResult> Index() {
            BaseResponse<IEnumerable<Donator>> donators = await donatorService.GetDonators(Convert.ToInt32(User.FindFirstValue("Id")));
            return View(donators);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> ReplenishWallet(SettingViewModel model) {
            model.Replenish.Created = DateTime.Now;
            model.Replenish.Received = model.Replenish.Sended-model.Replenish.Sended/100*model.Replenish.Comission;
            model.Replenish.StatusId = (int)Models.Enums.TransactionStatus.Sended;
            model.Replenish.UserId = Convert.ToInt32(User.FindFirstValue("Id"));

            await transactionService.CreateTransaction(model.Replenish);
            await walletService.ReplenishWallet(Convert.ToInt32(User.FindFirstValue("Id")), model.Replenish.Received);
            return RedirectToAction("Index","Setting");
        }

        [HttpPost]
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
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> WithdrawWallet(SettingViewModel model) {
            model.Withdraw.Created = DateTime.Now;
            model.Withdraw.Received = model.Withdraw.Sended - model.Withdraw.Sended / 100 * model.Withdraw.Comission;
            model.Withdraw.StatusId = (int)Models.Enums.TransactionStatus.Sended;
            model.Withdraw.UserId = Convert.ToInt32(User.FindFirstValue("Id"));

            await transactionService.CreateTransaction(model.Withdraw);
            await walletService.WithdrawWallet(Convert.ToInt32(User.FindFirstValue("Id")), model.Withdraw.Sended);
            return RedirectToAction("Index", "Setting");
        }
    }
}
