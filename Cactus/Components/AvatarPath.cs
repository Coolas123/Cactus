using Cactus.Models.Database;
using Cactus.Models.Responses;
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
        private readonly IProfileMaterialService profileMaterialService;
        public AvatarPath(IMemoryCache cache,
            IProfileMaterialService profileMaterialService) {
            this.cache = cache;
            this.profileMaterialService = profileMaterialService;
        }
        public async Task<IViewComponentResult> InvokeAsync(int id,string html="") {
            string path = "";
            BaseResponse<ProfileMaterial> result = await profileMaterialService.GetAvatarAsync(id);
            if(result.StatusCode==200) path=result.Data.Path;
            return new HtmlContentViewComponentResult(
                new HtmlString($"<img src=\"{path}\" width=\"50\" height=\"50\" {html}>"));
        }
    }
}
