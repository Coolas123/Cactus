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
        public async Task<IActionResult> Index(string UrlPage,int authorPage=1) {
            BaseResponse<User> user =await userService.GetUserByUrlPageAsync(UrlPage);
            BaseResponse<IEnumerable<AuthorSubscribe>> subList = await subscribeService.GetPagingSubscribersAsync(user.Data.Id, authorPage, PageSize);
            BaseResponse < IEnumerable < AuthorSubscribe >> allSub =await subscribeService.GetSubscribersAsync(user.Data.Id);
            if (subList.StatusCode == 200&& user.StatusCode==200) {
                return View(new PagingIndividualViewModel
                {
                    Authors = subList.Data,
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = authorPage,
                        ItemsPerPage = PageSize,
                        TotalItems = allSub.Data.Count()
                    },
                    CurrentUser = user.Data
                });
            }
            return View(new PagingIndividualViewModel {
                PagingInfo = new PagingInfo
                {
                    Description= subList.Description
                },
                CurrentUser = user.Data
            });
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
