﻿using Aspose.Email.Tools.Verifications;
using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;
using Cactus.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Security.Claims;

namespace Cactus.Controllers
{
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
        [HttpGet]
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

            profile.NewSettingViewModel = new NewSettingViewModel();

            profile.User= await userRepository.GetAsync(userId);
            if (User.IsInRole("Author")) {
                BaseResponse<Author> author = await authorService.GetAsync(userId);
                if (author.StatusCode == 200) {
                    profile.NewSettingViewModel.NewAuthorSettingViewModel = new NewAuthorSettingViewModel();
                    profile.NewSettingViewModel.NewAuthorSettingViewModel.Description = author.Data.Description;
                }
            }
            
            profile.NewSettingViewModel.IsSettingChanged = isSettingChanged;
            return View(profile);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeSettings(SettingViewModel model) {
            bool isSettingChanged=false;
            if (model.NewSettingViewModel.AvatarFile != null) {
                var image = Image.FromStream(model.NewSettingViewModel.AvatarFile.OpenReadStream());
                if (image.Width % image.Height != 0) {
                    ModelState.AddModelError("NewSettingViewModel.AvatarFile", "Авватарка должна иметь соотношение 1 к 1");
                }
                else {
                    int id = Convert.ToInt32(User.FindFirst("Id").Value);
                    await profileMaterialService.ChangeAvatarAsync(model.NewSettingViewModel.AvatarFile, id);
                    isSettingChanged = true;
                }
            }

            if (model.NewSettingViewModel.Email != null) {
                ValidationResult isValid;
                new EmailValidator().Validate(model.NewSettingViewModel.Email, ValidationPolicy.SyntaxAndDomain, out isValid);
                if (isValid.ReturnCode == ValidationResponseCode.DomainValidationFailed) {
                    ModelState.AddModelError("Email", "неверный домен почты");
                }
            }

            if (User.IsInRole("Author") && model.NewSettingViewModel.NewAuthorSettingViewModel != null) {
                ModelErrorsResponse<Author> authorResponseSetting = await authorService.ChangeSettingAsync(model.NewSettingViewModel.NewAuthorSettingViewModel, Convert.ToInt32(User.FindFirst("Id").Value));
                if (authorResponseSetting.StatusCode != 200 && authorResponseSetting.Descriptions != null) {
                    foreach(var e in authorResponseSetting.Descriptions) {
                        ModelState.AddModelError(e.Key, e.Value);
                    }
                }
            }
            BaseResponse<User> user = await userService.GetAsync(Convert.ToInt32(User.FindFirst("Id").Value));
            model.User = user.Data;

            foreach (var state in ModelState) {
                if (state.Key.Split('.').Contains("NewSettingViewModel")&&
                    state.Value.Errors.Count != 0) {
                    return View("Index", model);
                }
            }

            ModelErrorsResponse<ClaimsIdentity> result = await userService.
                ChangeSettingsAsync(model.NewSettingViewModel, Convert.ToInt32(User.FindFirst("Id").Value));
            if (result.StatusCode != StatusCodes.Status200OK) {
                foreach (var error in result.Descriptions) {
                    ModelState.AddModelError(error.Key, error.Value);
                }
            }

            if (result.Data != null) {
                await HttpContext.SignOutAsync();
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(result.Data));
                return RedirectToAction("Login", "Account");
            }
            if(result.IsSettingChanged) isSettingChanged=true;
            model.NewSettingViewModel.IsSettingChanged = isSettingChanged;

            return View("Index",model);
        }

        [HttpPost]
        [Authorize(Roles = "Patron,Moderator")]
        public async Task<IActionResult> RegisterAuthor(SettingViewModel model) {
            model.User =await userRepository.GetAsync(Convert.ToInt32(User.FindFirstValue("Id")));
            if (ModelState["RegisterAuthor.UrlPage"].Errors.Count==0) {
                int id = Convert.ToInt32(User.FindFirst("Id").Value);
                BaseResponse<ClaimsIdentity> result = await authorService.RegisterAuthor(model.RegisterAuthor, id);
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
