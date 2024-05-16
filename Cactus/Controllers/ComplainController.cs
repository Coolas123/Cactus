using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;
using Cactus.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cactus.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class ComplainController:Controller
    {
        private readonly IComplainService complainService;
        public ComplainController(IComplainService complainService) {
            this.complainService = complainService;
        }

        [Authorize(Roles ="Moderator")]
        public async Task<IActionResult> Index() {
            var date = DateTime.Now.AddYears(-10);
            BaseResponse<IEnumerable<Complain>> complains = await complainService.GetNotReviewedComplains(date);
            if (complains.StatusCode == 200) {
                return View(complains.Data);
            }
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> AddComplain(PagingAuthorViewModel model) {

            model.NewComplain.Created= DateTime.Now.ToUniversalTime();
            BaseResponse<bool> response = await complainService.AddComplain(model.NewComplain);
            if (response.StatusCode != 200) {
                ViewData["ComplainError"] = response.Description;
            }
            return Redirect(model.NewComplain.ReturnUrl);
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetComplain(int complainId) {

            return View();
        }
    }
}
