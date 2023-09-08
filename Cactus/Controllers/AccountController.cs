using Cactus.Models.Database;
using Cactus.Models;
using Cactus.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Cactus.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class AccountController : Controller
    {
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        private RoleManager<IdentityRole> roleManager;
        private string returnUrl { get; set; }

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager) {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [AllowAnonymous]
        public IActionResult LoginGoogle(string provider, string returnUrl) {
            if (User.Identity.IsAuthenticated) {
                return View(new Models.ViewModels.LoginGoogleModel { ReturnUrl = returnUrl });
            }
            var redirectUrl = Url.Action(nameof(loginReturn),"Account", new { returnUrl });
            var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        [AllowAnonymous]
        public async Task<IActionResult> loginReturn(string returnUrl) {
            var result = await signInManager.GetExternalLoginInfoAsync();
            if (result == null) {
                return RedirectToAction(nameof(Login));
            }
            var signInResult = await signInManager.ExternalLoginSignInAsync(result.LoginProvider, result.ProviderKey,false,false);
            if (signInResult.Succeeded) {
                return Redirect(returnUrl??"/");
            }
            return View(nameof(RegistrationGoogle),new RegisterGoogleModel
            {
                Email= result.Principal.FindFirstValue(ClaimTypes.Email),
                ReturnUrl = returnUrl
            });
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl) {
            if (!User.Identity.IsAuthenticated) {
                return View(new Models.ViewModels.LoginModel { ReturnUrl = returnUrl });
            }
            return Redirect(returnUrl ?? "/");
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(Models.ViewModels.LoginModel model, string returnUrl) {
            if (ModelState.IsValid) {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
                if (result.Succeeded) {
                    return Redirect(returnUrl ?? "/");
                }
                else {
                    ModelState.AddModelError(nameof(model.Email), "Неверный логин или пароль");
                }
            }
            else {
                ModelState.AddModelError(nameof(model.Email), "Неверный логин или пароль");
            }
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult Registration(string returnUrl) {
            if (!User.Identity.IsAuthenticated) {
                return View(new RegisterModel { ReturnUrl = returnUrl });
            }
            return Redirect(returnUrl ?? "/");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Registration(RegisterModel model, string returnUrl) {
            if (ModelState.IsValid) {
                User user = new User
                {
                    UserName = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Surname = model.Surname,
                    DateOfBirth = model.DateOfBirth,
                    Gender = model.Gender,
                    Email = model.Email
                };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded) {
                    await userManager.AddToRoleAsync(user, "User");
                    await signInManager.SignInAsync(user, false);
                    return Redirect(returnUrl ?? "/");
                }
            }
            return View(model);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult RegistrationGoogle(RegisterGoogleModel model) {
            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> RegistrationGoogleConfirmedAsync(RegisterGoogleModel model) {
            var result = await signInManager.GetExternalLoginInfoAsync();
            if (result == null) {
                return RedirectToAction(nameof(Login));
            }
            User user = new User
            {
                UserName = model.Email,
                FirstName = result.Principal.FindFirstValue(ClaimTypes.Name),
                LastName = result.Principal.FindFirstValue(ClaimTypes.GivenName),
                Gender = result.Principal.FindFirstValue(ClaimTypes.Gender)??"",
                Email = result.Principal.FindFirstValue(ClaimTypes.Email)
            };
            var resultCreate = await userManager.CreateAsync(user, model.Password);
            if (resultCreate.Succeeded) {
                var loginResult = await userManager.AddLoginAsync(user, result);
                if (loginResult.Succeeded) {
                    await userManager.AddToRoleAsync(user, "User");
                    await signInManager.SignInAsync(user, false);
                    return Redirect(model.ReturnUrl ?? "/");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> LogOut() {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
