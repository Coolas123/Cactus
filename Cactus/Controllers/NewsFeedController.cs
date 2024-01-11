using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cactus.Controllers
{
    public class NewsFeedController:Controller
    {
        [Authorize(Roles = "User")]
        [Authorize(Roles = "Patron")]
        public IActionResult Index() {
            return View();
        }
    }
}
