using Microsoft.AspNetCore.Mvc;

namespace Cactus.Controllers
{
    public class PatronController : Controller
    {
        public IActionResult Index() {
            return View();
        }
    }
}
