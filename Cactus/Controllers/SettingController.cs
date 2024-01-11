using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cactus.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class SettingController:Controller
    {
        [Authorize(Roles = "User")]
        [Authorize(Roles = "Patron")]
        public IActionResult Index() {
            return View();
        }
    }
}
