using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cactus.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class HomeController: Controller
    {
        public IActionResult Index() {
            return View();
        }

        [Authorize(Roles = "User")]
        [Authorize(Roles = "Patron,Individual")]
        [HttpGet]
        public IActionResult Logged() {
            return View();
        }
    }
}
