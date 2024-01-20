using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;
using Cactus.Services.Implementations;
using Cactus.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

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
