using Cactus.Models.Responses;
using Cactus.Services.Interfaces;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace Cactus.Components
{
    public class Wallet : ViewComponent
    {
        private readonly IWalletService walletService;
        public Wallet(IWalletService walletService) {
            this.walletService = walletService;
        }
        public async Task<IViewComponentResult> InvokeAsync(int id) {
            BaseResponse<Models.Database.Wallet> wallet = await walletService.GetWallet(id);
            return new HtmlContentViewComponentResult(
                new HtmlString($"<li class=\"text-white\" style=\"font-size: 20px; margin-right:10px;margin-left:5px \">{wallet.Data?.Balance}</li>"));
        }
    }
}
