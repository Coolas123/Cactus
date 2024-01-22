using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;
using Cactus.Services.Interfaces;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Cactus.Components
{
    public class BannerPath:ViewComponent
    {
        private readonly IMemoryCache cache;
        private readonly IMaterialService materialService;
        public BannerPath(IMemoryCache cache, IIndividualRepository individualRepository,
            IMaterialService materialService) {
            this.cache = cache;
            this.materialService = materialService;
        }
        public async Task<IViewComponentResult> InvokeAsync(int id, string html = "") {
            IndividualProfileViewModel profile;
            cache.TryGetValue("IndividualProfile", out profile);
            if (profile == null)
                profile = new IndividualProfileViewModel();
            if (profile.BannerPath == null) {
                BaseResponse<Material> result = await materialService.GetBannerAsync(id);
                if (result.StatusCode == StatusCodes.Status200OK) {
                    profile.BannerPath = result.Data.Path;
                    cache.Set("IndividualProfile", profile);
                }
            }
            return new HtmlContentViewComponentResult(
                new HtmlString($"<img src=\"{profile.BannerPath}\" class=\"banner my-2\" style=\"width:100%; height:250px;\" {html}>"));
        }
    }
}
