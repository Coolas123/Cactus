using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;
using Cactus.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Claims;

namespace Cactus.Controllers
{
    [Authorize(Roles = "User")]
    [Authorize(Roles = "Patron,Individual,Legal")]
    [AutoValidateAntiforgeryToken]
    public class SettingController:Controller
    {
        private readonly IProfileMaterialService profileMaterialService;
        private readonly IUserRepository userRepository;
        private readonly IUserService userService;
        private readonly IIndividualService individualService;
        private readonly ILegalService legalService;
        private readonly IMemoryCache cache;

        public SettingController(IProfileMaterialService profileMaterialService, IUserService userService,
            IUserRepository userRepository, IMemoryCache cache, IIndividualService individualService,
            ILegalService legalService) {
            this.profileMaterialService = profileMaterialService;
            this.userService = userService;
            this.userRepository = userRepository;
            this.cache = cache;
            this.individualService = individualService;
            this.legalService = legalService;
        }
        public async Task<IActionResult> Index() {
            SettingViewModel profile= new SettingViewModel();
            int userId = Convert.ToInt32(User.FindFirstValue("Id"));
            BaseResponse<ProfileMaterial> response = await profileMaterialService.GetAvatarAsync(userId);
            if (response.StatusCode == StatusCodes.Status200OK) {
                profile.AvatarPath = response.Data.Path;
            }
            if (!User.IsInRole("Patron")) {
                BaseResponse<ProfileMaterial> banner = await profileMaterialService.GetBannerAsync(User.Identity.Name);
                if (banner.StatusCode == StatusCodes.Status200OK) {
                    profile.BannerPath = banner.Data.Path;
                }
            }
            profile.User= await userRepository.GetAsync(userId);
            if (User.IsInRole("Individual")) {
                BaseResponse<Individual> individual = await individualService.GetAsync(userId);
                if(individual.StatusCode==200)
                    profile.Individual = individual.Data;
            }
            if (User.IsInRole("Legal")) {
                BaseResponse<Legal> legal = await legalService.GetAsync(userId);
                if (legal.StatusCode == 200)
                    profile.Legal = legal.Data;
            }
            return View(profile);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeSettings(SettingViewModel model) {
            if (model.AvatarFile != null) {
                int id = Convert.ToInt32(User.FindFirst("Id").Value);
                await profileMaterialService.ChangeAvatarAsync(model.AvatarFile, id);
            }

            if (User.IsInRole("Individual")) {
                if (model.BannerFile != null) {
                    int id = Convert.ToInt32(User.FindFirst("Id").Value);
                    await profileMaterialService.ChangeBannerAsync(model.BannerFile, id);
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
            if (ModelState["RegisterIndividual.UrlPage"].Errors.Count==0) {
                int id = Convert.ToInt32(User.FindFirst("Id").Value);
                BaseResponse<ClaimsIdentity> result = await individualService.RegisterIndividual(model, id);
                if (result.StatusCode != StatusCodes.Status200OK) {
                    ModelState.AddModelError(nameof(model.RegisterIndividual.UrlPage), result.Description);
                    return View("Index", model);
                }
                await HttpContext.SignOutAsync();
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(result.Data));
                return RedirectToAction("Index", "Individual", new { UrlPage = model.RegisterIndividual.UrlPage });
            }
            return View("Index", model);
        }

        [HttpPost]
        [Authorize(Roles = "Individual")]
        public async Task<IActionResult> RegisterLegal(SettingViewModel model) {
            model.User = await userRepository.GetAsync(Convert.ToInt32(User.FindFirstValue("Id")));
            if (ModelState["RegisterLegal.UrlPage"].Errors.Count == 0) {
                int id = Convert.ToInt32(User.FindFirst("Id").Value);
                //
                BaseResponse<ClaimsIdentity> result = await legalService.RegisterLegal(model.RegisterLegal, id);
                if (result.StatusCode != StatusCodes.Status200OK) {
                    ModelState.AddModelError(nameof(model.RegisterLegal.UrlPage), result.Description);
                    return View("Index", model);
                }
                await HttpContext.SignOutAsync();
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(result.Data));
                return RedirectToAction("Index", "Legal", new { UrlPage = model.RegisterLegal.UrlPage });
            }
            return View("Index", model);
        }
    }
}
