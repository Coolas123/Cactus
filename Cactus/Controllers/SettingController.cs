using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;
using Cactus.Services.Implementations;
using Cactus.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Cactus.Infrastructure.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System.IO;

namespace Cactus.Controllers
{
    [Authorize(Roles = "User")]
    [Authorize(Roles = "Patron,Individual")]
    [AutoValidateAntiforgeryToken]
    public class SettingController:Controller
    {
        private readonly IMaterialService materialService;
        private readonly IUserRepository userRepository;
        private readonly IUserService userService;
        private readonly IIndividualService individualService;
        private readonly IMemoryCache cache;

        public SettingController(IMaterialService materialService, IUserService userService,
            IUserRepository userRepository, IMemoryCache cache, IIndividualService individualService) {
            this.materialService = materialService;
            this.userService = userService;
            this.userRepository = userRepository;
            this.cache = cache;
            this.individualService = individualService;
        }
        public async Task<IActionResult> Index() {
            SettingViewModel profile= new SettingViewModel();
            BaseResponse<Material> response = await materialService.GetAvatarAsync(User.Identity.Name);
            if (response.StatusCode == StatusCodes.Status200OK) {
                profile.AvatarPath = response.Data.Path;
            }
            BaseResponse<Material> banner = await materialService.GetBannerAsync(User.Identity.Name);
            if (banner.StatusCode == StatusCodes.Status200OK) {
                profile.BannerPath = banner.Data.Path;
            }
            profile.User= await userRepository.GetAsync(Convert.ToInt32(User.FindFirst("Id").Value));
            return View(profile);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeSettings(SettingViewModel model) {
            if (model.AvatarFile != null) {
                int id = Convert.ToInt32(User.FindFirst("Id").Value);
                await materialService.ChangeAvatarAsync(model.AvatarFile, id);
            }

            if (User.IsInRole("Individual")) {
                if (model.BannerFile != null) {
                    int id = Convert.ToInt32(User.FindFirst("Id").Value);
                    await materialService.ChangeBannerAsync(model.BannerFile, id);
                }
            }

            ModelErrorsResponse<ClaimsIdentity> result = await userService.
                ChangeSettingsAsync(model, Convert.ToInt32(User.FindFirst("Id").Value));
            if (result.StatusCode != StatusCodes.Status200OK) {
                foreach (var error in result.Descriptions) {
                    ModelState.AddModelError(error.Key, error.Value);
                }
            }
            model.User = await userRepository.GetAsync(Convert.ToInt32(User.FindFirst("Id").Value));

            if (result.Data != null) {
                await HttpContext.SignOutAsync();
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(result.Data));
                return RedirectToAction("Login", "Account");
            }
            return View("Index",model);
        }

        [HttpPost]
        [Authorize(Roles = "Patron")]
        public async Task<IActionResult> RegisterIndividual(SettingViewModel model) {
            model.User =await userRepository.GetAsync(Convert.ToInt32(User.FindFirstValue("Id")));
            if (ModelState["IndividualSettings.UrlPage"].Errors.Count==0) {
                int id = Convert.ToInt32(User.FindFirst("Id").Value);
                BaseResponse<ClaimsIdentity> result = await individualService.RegisterIndividual(model, id);
                if (result.StatusCode != StatusCodes.Status200OK) {
                    ModelState.AddModelError(nameof(model.IndividualSettings.UrlPage), result.Description);
                    return View("Index", model);
                }
                await HttpContext.SignOutAsync();
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(result.Data));
                return RedirectToAction("Index", "Individual", new { UrlPage = model.IndividualSettings.UrlPage });
            }
            return View("Index", model);
        }
    }
}
