using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;
using Cactus.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Cactus.Controllers
{
    [Authorize(Roles = "User")]
    [Authorize(Roles = "Patron,Author")]
    [AutoValidateAntiforgeryToken]
    public class SettingController:Controller
    {
        private readonly IProfileMaterialService profileMaterialService;
        private readonly IUserRepository userRepository;
        private readonly IUserService userService;
        private readonly IAuthorService authorService;

        public SettingController(IProfileMaterialService profileMaterialService, IUserService userService,
            IUserRepository userRepository, IAuthorService authorService) {

            this.profileMaterialService = profileMaterialService;
            this.userService = userService;
            this.userRepository = userRepository;
            this.authorService = authorService;
        }
        public async Task<IActionResult> Index(bool isSettingChanged=false) {
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
            if (User.IsInRole("Author")) {
                BaseResponse<Author> author = await authorService.GetAsync(userId);
                if(author.StatusCode==200)
                    profile.Author = author.Data;
            }
            
            profile.IsSettingChanged = isSettingChanged;
            return View(profile);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeSettings(SettingViewModel model) {
            bool isSettingChanged=false;
            if (model.AvatarFile != null) {
                int id = Convert.ToInt32(User.FindFirst("Id").Value);
                await profileMaterialService.ChangeAvatarAsync(model.AvatarFile, id);
                isSettingChanged = true;
            }

            if (User.IsInRole("Author")) {
                if (model.BannerFile != null) {
                    int id = Convert.ToInt32(User.FindFirst("Id").Value);
                    await profileMaterialService.ChangeBannerAsync(model.BannerFile, id);
                    isSettingChanged = true;
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
            if(result.IsSettingChanged) isSettingChanged=true;
            model.IsSettingChanged = isSettingChanged;
            return RedirectToAction("Index", new { isSettingChanged = isSettingChanged });
        }

        [HttpPost]
        [Authorize(Roles = "Patron")]
        public async Task<IActionResult> RegisterAuthor(SettingViewModel model) {
            model.User =await userRepository.GetAsync(Convert.ToInt32(User.FindFirstValue("Id")));
            if (ModelState["RegisterAuthor.UrlPage"].Errors.Count==0) {
                int id = Convert.ToInt32(User.FindFirst("Id").Value);
                BaseResponse<ClaimsIdentity> result = await authorService.RegisterAuthor(model, id);
                if (result.StatusCode != StatusCodes.Status200OK) {
                    ModelState.AddModelError(nameof(model.RegisterAuthor.UrlPage), result.Description);
                    return View("Index", model);
                }
                await HttpContext.SignOutAsync();
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(result.Data));
                return RedirectToAction("Index", "Author", new { UrlPage = model.RegisterAuthor.UrlPage });
            }
            return View("Index", model);
        }
    }
}
