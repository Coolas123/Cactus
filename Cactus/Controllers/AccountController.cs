using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;
using Cactus.Services.Implementations;
using Cactus.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Claims;

namespace Cactus.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class AccountController : Controller
    {
        private readonly IUserService userService;
        private readonly IMaterialService materialService;
        private readonly IIndividualService individualService;
        private readonly IMemoryCache cache;


        public AccountController (IUserService userService, IMemoryCache cache,
            IMaterialService materialService, IIndividualService individualService)
        {
            this.userService = userService;
            this.materialService = materialService;
            this.individualService = individualService;
            this.cache = cache;
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

        [Authorize(Roles = "Patron")]
        public IActionResult RegisterIndividual() {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Patron")]
        public async Task<IActionResult> RegisterIndividual(RegisterIndividualViewModel model) {
            if(ModelState.IsValid) {
                int id = Convert.ToInt32(User.FindFirst("Id").Value);
                BaseResponse<ClaimsIdentity> result = await individualService.RegisterIndividual(model, id);
                if (result.StatusCode != StatusCodes.Status200OK) {
                    ModelState.AddModelError(nameof(model.UrlPage), result.Description);
                    return View(model);
                }
                await HttpContext.SignOutAsync();
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(result.Data));
                return RedirectToAction("Index", "Individual");
            }
            return View(model);
        }

        public async Task<IActionResult> LogOut() {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            cache.Remove("AvatarPath");
            return RedirectToAction("Index", "Home");
        }
    }
}
