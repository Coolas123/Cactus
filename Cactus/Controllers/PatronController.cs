using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;
using Cactus.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cactus.Controllers
{
    [AllowAnonymous]
    [Route("Patron")]
    public class PatronController : Controller
    {
        private int PageSize = 4;
        private readonly IAuthorSubscribeService authorSubscribeService;
        private readonly IUserService userService;
        public PatronController(IAuthorSubscribeService authorSubscribeService, IUserService userService) {
            this.authorSubscribeService = authorSubscribeService;
            this.userService = userService;
        }

        [Route("{id}")]
        public async Task<IActionResult> Index(int id, int authorPage = 1) {
            var response = new PagingPatronViewModel();
            BaseResponse<User> patron = await userService.GetAsync(id);
            if (patron.StatusCode == 200) {
                BaseResponse<PagingAuthorViewModel> subs = await authorSubscribeService.GetUserViewSubscribersAsync(patron.Data.Id, authorPage, PageSize);
                if (subs.StatusCode == 200) {
                    response.SubscribesPagingInfo = subs.Data.SubscribesPagingInfo;
                    response.AuthorSubscribe = subs.Data.Authors;
                }
            }
            response.CurrentUser = patron.Data;
            return View(response);
        }
    }
}
