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
    [AutoValidateAntiforgeryToken]
    public class WalletController: Controller
    {
        private readonly ITransactionService transactionService;
        private readonly IWalletService walletService;
        private readonly IDonatorService donatorService;
        private readonly IAuthorService authorService;
        public WalletController(ITransactionService transactionService, IWalletService walletService,
            IDonatorService donatorService, IAuthorService authorService) {
            this.transactionService = transactionService;
            this.walletService = walletService;
            this.donatorService = donatorService;
            this.authorService = authorService;
        }
        public async Task<IActionResult> Index() {
            BaseResponse<IEnumerable<Donator>> donators = await donatorService.GetDonators(Convert.ToInt32(User.FindFirstValue("Id")));
            return View(donators);
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
