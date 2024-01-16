using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cactus.Controllers
{
    [Authorize(Roles = "User")]
    [Authorize(Roles = "Patron")]
    [AutoValidateAntiforgeryToken]
    public class MessageController : Controller
    {
        public IActionResult Index() {
            return View();
        }
    }
}
