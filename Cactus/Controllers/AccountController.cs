﻿using Aspose.Email.Tools.Verifications;
using Cactus.Models.ViewModels;
using Cactus.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Cactus.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class AccountController : Controller
    {
        private readonly IUserService userService;


        public AccountController (IUserService userService)
        {
            this.userService = userService;
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl) {
            if (!User.Identity.IsAuthenticated) {
                return View(new LoginViewModel { ReturnUrl = returnUrl });
            }
            return Redirect(returnUrl ?? "/");
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl) {
            if (ModelState.IsValid) {
                var result = await userService.Login(model);
                if (result.StatusCode == StatusCodes.Status200OK) {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(result.Data));
                    return Redirect(returnUrl ?? "/");
                }
                else {
                    ModelState.AddModelError(nameof(model.Email), result.Description);
                }
            }
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult Registration(string returnUrl) {
            if (!User.Identity.IsAuthenticated) {
                return View(new RegisterViewModel { ReturnUrl = returnUrl });
            }
            return Redirect(returnUrl ?? "/");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Registration(RegisterViewModel model, string returnUrl) {
            if (model.Email != null) {
                ValidationResult isValid;
                new EmailValidator().Validate(model.Email, ValidationPolicy.SyntaxAndDomain, out isValid);
                if (isValid.ReturnCode == ValidationResponseCode.DomainValidationFailed) {
                    ModelState.AddModelError("Email", "неверный домен почты");
                }
            }

            if (ModelState.IsValid) {
                var result = await userService.Register(model);
                if (result.StatusCode==StatusCodes.Status200OK) {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(result.Data));
                    return Redirect(returnUrl ?? "/");
                }
                ModelState.AddModelError("",result.Description);
            }
            return View(model);
        }

        public async Task<IActionResult> LogOut() {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "NewsFeed");
        }
    }
}
