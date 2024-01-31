using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;
using Cactus.Services.Implementations;
using Cactus.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace Cactus.Controllers
{
    [Authorize(Roles = "Individual")]
    [Route("Individual")]
    [AutoValidateAntiforgeryToken]
    public class IndividualController:Controller
    {
        private int PageSize = 4;
        private readonly ISubscribeService subscribeService;
        private readonly IUserService userService;
        private readonly IIndividualService individualService;
        public IndividualController(ISubscribeService subscribeService, IUserService userService,
            IIndividualService individualService) {
            this.subscribeService = subscribeService;
            this.userService = userService;
            this.individualService = individualService;
        }

        [Route("{UrlPage}")]
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
    }
}
