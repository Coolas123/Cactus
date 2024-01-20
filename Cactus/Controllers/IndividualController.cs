using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cactus.Controllers
{
    [Authorize(Roles = "Individual")]
    [AutoValidateAntiforgeryToken]
    public class IndividualController:Controller
    {
        [Route("/{UrlPage}")]
        public IActionResult Index(string UrlPage) {
            return View();
        }
    }
}
