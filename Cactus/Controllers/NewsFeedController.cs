﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cactus.Controllers
{
    [Authorize(Roles = "User")]
    [Authorize(Roles = "Patron")]
    public class NewsFeedController:Controller
    {
        public IActionResult Index() {
            return View();
        }
    }
}
