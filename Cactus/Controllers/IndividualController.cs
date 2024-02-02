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
        private readonly ISubscribeService subscribeService;
        private readonly IUserService userService;
        private readonly IPostService postService;
        private readonly LinkGenerator linkGenerator;
        private readonly IIndividualService individualService;
        public IndividualController(ISubscribeService subscribeService, IUserService userService,
           IPostService postService, LinkGenerator linkGenerator, IIndividualService individualService) {
            this.subscribeService = subscribeService;
            this.userService = userService;
            this.postService = postService;
            this.linkGenerator = linkGenerator;
            this.individualService = individualService;
        }

        [Route("{UrlPage}")]
        [Authorize(Roles = "Individual,Patron")]
        public async Task<IActionResult> Index(string UrlPage,int authorPage=1,int postPage=1) {
            BaseResponse<User> user =await userService.GetUserByUrlPageAsync(UrlPage);
            BaseResponse<IEnumerable<AuthorSubscribe>> subList = await subscribeService.GetPagingSubscribersAsync(user.Data.Id, authorPage, PageSize);
            BaseResponse <IEnumerable<AuthorSubscribe>> allSub =await subscribeService.GetSubscribersAsync(user.Data.Id);
            BaseResponse <IEnumerable<Post>> postList =await postService.GetPagingPostsAsync(user.Data.Id, postPage, PageSize);
            BaseResponse <IEnumerable<Post>> allPost =await postService.GetPostsAsync(user.Data.Id);
            
            var response = new PagingIndividualViewModel();
            if (subList.StatusCode == 200 && user.StatusCode == 200) {
                response.Authors = subList.Data;
                response.SubscribesPagingInfo = new PagingInfo
                {
                    CurrentPage = authorPage,
                    ItemsPerPage = PageSize,
                    TotalItems = allSub.Data.Count()
                };
                response.CurrentUser = user.Data;
            }
            else {
                response.SubscribesPagingInfo = new PagingInfo
                {
                    Description = subList.Description
                };
                response.CurrentUser = user.Data;
            }
            if (postList.StatusCode == 200 && user.StatusCode == 200) {
                response.Posts = postList.Data;
                response.PostsPagingInfo = new PagingInfo
                {
                    CurrentPage = postPage,
                    ItemsPerPage = PageSize,
                    TotalItems = allPost.Data.Count()
                };
                response.CurrentUser = user.Data;
            }
            else {
                response.PostsPagingInfo = new PagingInfo
                {
                    Description = postList.Description
                };
                response.CurrentUser = user.Data;
            }
            return View(response);
        }

        [HttpPost]
        [Authorize(Roles = "Individual")]
        public async Task<IActionResult> AddPost(PagingIndividualViewModel model) {
            await postService.AddPost(model.Post, Convert.ToInt32(User.FindFirstValue("Id")));
            BaseResponse<Individual> response = await individualService.GetAsync(Convert.ToInt32(User.FindFirstValue("Id")));
            string path = "";
            if (response.StatusCode==200)
                path = linkGenerator.GetPathByAction("Index", "Individual", new { UrlPage = response.Data.UrlPage })!;
            return Redirect(path); 
        }
    }
}
