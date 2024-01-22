using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;
using Cactus.Services.Interfaces;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.Caching.Memory;

namespace Cactus.Components
{
    public class AvatarPath : ViewComponent
    {
        private readonly IMemoryCache cache;
        private readonly IMaterialService materialService;
        public AvatarPath(IMemoryCache cache, IIndividualRepository individualRepository,
            IMaterialService materialService) {
            this.cache = cache;
            this.materialService = materialService;
        }
        public async Task<IViewComponentResult> InvokeAsync(int id,string html="") {
            IndividualProfileViewModel profile;
            cache.TryGetValue("IndividualProfile", out profile);
            if (profile == null)
                profile = new IndividualProfileViewModel();
            if (profile.AvatarPath == null) {
                BaseResponse<Material> result = await materialService.GetAvatarAsync(id);
                if (result.StatusCode == StatusCodes.Status200OK) {
                    profile.AvatarPath = result.Data.Path;
                }
            }
            cache.Set("IndividualProfile", profile);
            return new HtmlContentViewComponentResult(
                new HtmlString($"<img src=\"{profile.AvatarPath}\" class=\"avatar\" width=\"50\" height=\"50\" {html}>"));
        }
    }
}
