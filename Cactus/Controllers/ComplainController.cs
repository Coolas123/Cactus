using Cactus.Models.Responses;
using Cactus.Models.ViewModels;
using Cactus.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cactus.Controllers
{
    public class ComplainController:Controller
    {
        private readonly IComplainService complainService;
        public ComplainController(IComplainService complainService) {
            this.complainService = complainService;
        }
        public async Task<IActionResult> Index() {
            var date = DateTime.Now.AddYears(-10);
            BaseResponse<IEnumerable<ComplainViewModel>> complains = await complainService.GetNotReviewedComplains(5, date);
            if (complains.StatusCode == 200) {
                return View(complains.Data);
            }
            return View();
        }
    }
}
