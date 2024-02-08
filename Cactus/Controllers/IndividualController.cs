using Cactus.Components;
using Cactus.Infrastructure.Repositories;
using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;
using Cactus.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using SportsStore.Models;
using System.IO;
using System.Security.Claims;

namespace Cactus.Controllers
{
    [Route("Individual")]
    [AutoValidateAntiforgeryToken]
    public class IndividualController:Controller
    {
        private int PageSize = 4;
        private readonly IAuthorSubscribeService authorSubscribeService;
        private readonly IPostService postService;
        private readonly LinkGenerator linkGenerator;
        private readonly IIndividualService individualService;
        public IndividualController(IAuthorSubscribeService authorSubscribeService,
           IPostService postService, LinkGenerator linkGenerator, IIndividualService individualService) {
            this.authorSubscribeService = authorSubscribeService;
            this.postService = postService;
            this.linkGenerator = linkGenerator;
            this.individualService = individualService;
        }

        [Route("{UrlPage}")]
        [AllowAnonymous]
        public async Task<IActionResult> Index(string UrlPage,int authorPage=1,int postPage=1) {
            var response = new PagingAuthorViewModel();
            BaseResponse<User> user =await individualService.GetUserByUrlPageAsync(UrlPage);
            if (user.StatusCode == 200) {
                BaseResponse<PagingAuthorViewModel> subs = await authorSubscribeService.GetUserViewSubscribersAsync(user.Data.Id, authorPage, PageSize);
                if (subs.StatusCode == 200) {
                    response.SubscribesPagingInfo = subs.Data.SubscribesPagingInfo;
                    response.Authors = subs.Data.Authors;
                }

                BaseResponse<PagingAuthorViewModel> posts = await postService.GetUserViewPostsAsync(user.Data.Id, postPage, PageSize);
                if (posts.StatusCode == 200) {
                    response.PostsPagingInfo= posts.Data.PostsPagingInfo;
                    response.Posts= posts.Data.Posts;
                }
            }
            response.CurrentUser = user.Data;
            return View(response);
        }

        [HttpPost]
        [Authorize(Roles = "Individual")]
        public async Task<IActionResult> AddPost(PagingAuthorViewModel model) {
            await postService.AddPost(model.Post, Convert.ToInt32(User.FindFirstValue("Id")));
            BaseResponse<Individual> response = await individualService.GetAsync(Convert.ToInt32(User.FindFirstValue("Id")));
            string path = "";
            if (response.StatusCode==200)
                path = linkGenerator.GetPathByAction("Index", "Individual", new { UrlPage = response.Data.UrlPage })!;
            return Redirect(path); 
        }
    }
}
