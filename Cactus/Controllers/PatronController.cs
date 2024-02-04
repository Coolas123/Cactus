using Cactus.Components;
using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;
using Cactus.Services.Implementations;
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
        private readonly IProjectSubscribeService projectSubscribeService;
        private readonly IUserService userService;
        private readonly IPostService postService;
        private readonly LinkGenerator linkGenerator;
        private readonly IPatronService patronService;
        public PatronController(IAuthorSubscribeService authorSubscribeService, IUserService userService,
           IPostService postService, LinkGenerator linkGenerator, IPatronService patronService,
           IProjectSubscribeService projectSubscribeService) {
            this.authorSubscribeService = authorSubscribeService;
            this.userService = userService;
            this.postService = postService;
            this.linkGenerator = linkGenerator;
            this.patronService = patronService;
            this.projectSubscribeService = projectSubscribeService;
        }

        [Route("id{id}")]
        public async Task<IActionResult> Index(int id, int authorPage = 1, int projectPage = 1) {
            var response = new PagingPatronViewModel();
            BaseResponse<User> patron = await userService.GetAsync(id);
            if (patron.StatusCode == 200) {
                BaseResponse<PagingAuthorViewModel> subs = await authorSubscribeService.GetUserViewSubscribersAsync(patron.Data.Id, authorPage, PageSize);
                if (subs.StatusCode == 200) {
                    response.SubscribesPagingInfo = subs.Data.SubscribesPagingInfo;
                    response.AuthorSubscribe = subs.Data.Authors;
                }
                BaseResponse<PagingPatronViewModel> projects = await projectSubscribeService.GetUserViewProjectsAsync(patron.Data.Id, projectPage, PageSize);
                if (projects.StatusCode == 200) {
                    response.SubscribesPagingInfo = projects.Data.SubscribesPagingInfo;
                    response.AuthorSubscribe = projects.Data.AuthorSubscribe;
                }
            }
            response.CurrentUser = patron.Data;
            return View(response);
        }
    }
}
