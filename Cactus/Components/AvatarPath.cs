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
            string path = "";
            BaseResponse<Material> result = await materialService.GetAvatarAsync(id);
            if(result.StatusCode==200) path=result.Data.Path;
            return new HtmlContentViewComponentResult(
                new HtmlString($"<img src=\"{path}\" width=\"50\" height=\"50\" {html}>"));
        }
    }
}
