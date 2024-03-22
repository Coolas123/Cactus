using Cactus.Models.ViewModels;
using Cactus.Services.Implementations;
using Cactus.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Cactus.Controllers
{
    [Authorize(Roles = "Author")]
    [AutoValidateAntiforgeryToken]
    public class WalletController: Controller
    {
        private readonly ITransactionService transactionService;
        private readonly IWalletService walletService;
        public WalletController(ITransactionService transactionService, IWalletService walletService) {
            this.transactionService = transactionService;
            this.walletService = walletService;
        }
        public async Task<IActionResult> Index() {

            return View();
        }

        public async Task<IActionResult> ReplenishWallet(SettingViewModel model) {
            model.NewTransaction.Created = DateTime.Now;
            model.NewTransaction.Received = model.NewTransaction.Sended-model.NewTransaction.Sended/100;
            model.NewTransaction.StatusId = (int)Models.Enums.TransactionStatus.Sended;
            model.NewTransaction.UserId = Convert.ToInt32(User.FindFirstValue("Id"));
            await transactionService.CreateTransaction(model.NewTransaction);
            await walletService.ReplenishWallet(Convert.ToInt32(User.FindFirstValue("Id")), model.NewTransaction.Received);
            return RedirectToAction("Index","Setting");
        }
    }
}
