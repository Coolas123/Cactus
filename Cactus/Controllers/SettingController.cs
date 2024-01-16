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

namespace Cactus.Controllers
{
    [Authorize(Roles = "User")]
    [Authorize(Roles = "Patron")]
    [AutoValidateAntiforgeryToken]
    public class SettingController:Controller
    {
        private readonly IMaterialService materialService;
        private readonly IUserRepository userRepository;
        private readonly IUserService userService;

        public SettingController(IMaterialService materialService, IUserService userService, IUserRepository userRepository) {
            this.materialService = materialService;
            this.userService = userService;
            this.userRepository = userRepository;
        }
        public async Task<IActionResult> Index() {
            ProfileViewModel profile= new ProfileViewModel();
            BaseResponse<Material> response = await materialService.GetAvatarAsync(User.Identity.Name);
            if (response.StatusCode == StatusCodes.Status200OK) {
                profile.AvatarPath = response.Data.Path;
            }
            profile.User= await userRepository.GetAsync(Convert.ToInt32(User.FindFirst("Id").Value));
            return View(profile);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeSettings(ProfileViewModel model) {
            if (model.AvatarFile != null) {
                string email = User.Identity.Name;
                await materialService.ChangeAvatarAsync(model.AvatarFile, email);
            }

            ModelErrorsResponse<ClaimsIdentity> result =await userService.
                ChangeSettingsAsync(model,Convert.ToInt32(User.FindFirst("Id").Value));
            if (result.StatusCode != StatusCodes.Status200OK) {
                foreach(var error in result.Descriptions) {
                    ModelState.AddModelError(error.Key,error.Value);
                }
            }

            if (result.Data != null) {
                await HttpContext.SignOutAsync();
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(result.Data));
                return RedirectToAction("Login", "Account");
            }
            model.User =await userRepository.GetAsync(Convert.ToInt32(User.FindFirst("Id").Value));
            return View("Index",model);
        }
    }
}
