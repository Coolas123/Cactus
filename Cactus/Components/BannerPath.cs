using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Services.Interfaces;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.Caching.Memory;

namespace Cactus.Components
{
    public class BannerPath:ViewComponent
    {
        private readonly IMemoryCache cache;
        private readonly IProfileMaterialService profileMaterialService;
        public BannerPath(IMemoryCache cache,
            IProfileMaterialService profileMaterialService) {
            this.cache = cache;
            this.profileMaterialService = profileMaterialService;
        }
        public async Task<IViewComponentResult> InvokeAsync(int id, string html = "") {
            string path = "";
            BaseResponse<ProfileMaterial> result = await profileMaterialService.GetBannerAsync(id);
            if (result.StatusCode == 200) path = result.Data.Path;
            else path = "/banner.png";
            return new HtmlContentViewComponentResult(
                    new HtmlString($"<img src=\"{path}\" class=\"banner my-2\" {html}>"));
        }
    }
}
