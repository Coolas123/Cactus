﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Cactus.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class HomeController:Controller
    {
        public IActionResult Index() {
            return View();
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        public IActionResult Logged() {
            return View();
        }
    }
}
