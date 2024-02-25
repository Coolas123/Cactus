using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;
using Cactus.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nest;

namespace Cactus.Controllers
{
    [Authorize(Roles = "User")]
    [Authorize(Roles = "Patron,Author")]
    public class NewsFeedController:Controller
    {
        private readonly INewsFeedService newsFeedService;
        public NewsFeedController(INewsFeedService newsFeedService) {
            this.newsFeedService = newsFeedService;
        }
        public IActionResult Index() {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Search(SearchViewModel model) {
            BaseResponse<SearchResultViewModel> result = await newsFeedService.Search(model);
            if (result.StatusCode == 200) {
                model.searchResponse = result.Data;
            }
            return View("Index", model);
        }
    }
}
