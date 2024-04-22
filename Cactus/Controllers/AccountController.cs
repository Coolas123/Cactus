using Cactus.Models.ViewModels;
using Cactus.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Sockets;
using System.Net;
using System.Security.Claims;
using Aspose.Email.Tools.Verifications;
using System.ComponentModel.DataAnnotations;

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
            EmailValidator validator = new EmailValidator();
            Aspose.Email.Tools.Verifications.ValidationResult isValid;
            validator.Validate(model.Email, ValidationPolicy.SyntaxAndDomain, out isValid);
            if (isValid.ReturnCode == ValidationResponseCode.DomainValidationFailed) {
                ModelState.AddModelError("Email","неверный домен почты");
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
            return RedirectToAction("Index", "Home");
        }
    }
}
