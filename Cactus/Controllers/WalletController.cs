using Cactus.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cactus.Controllers
{
    [Authorize(Roles = "Author")]
    [AutoValidateAntiforgeryToken]
    public class WalletController: Controller
    {
        public async Task<IActionResult> Index() {

            return View();
        }

        public async Task<IActionResult> ReplenishWallet(WalletViewModel model) {

            return RedirectToAction("Index","Setting");
        }
    }
}
