using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cactus.Controllers
{
    [Authorize(Roles = "Legal")]
    [Route("Legal")]
    [AutoValidateAntiforgeryToken]
    public class LegalController: Controller
    {
        [Route("{UrlPage}")]
        [Authorize(Roles = "Individual,Patron,Legal")]
        public IActionResult Index(string UrlPage) {
            return View();
        }
    }
}
