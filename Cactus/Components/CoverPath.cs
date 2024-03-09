using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Services.Interfaces;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.Caching.Memory;

namespace Cactus.Components
{
    public class CoverPath : ViewComponent
    {
        private readonly ISubLevelMaterialService subLevelMaterialService;
        public CoverPath(ISubLevelMaterialService subLevelMaterialService) {
            this.subLevelMaterialService = subLevelMaterialService;
        }
        public async Task<IViewComponentResult> InvokeAsync(int id, string html = "") {
            string path = "";
            BaseResponse<SubLevelMaterial> result = await subLevelMaterialService.GetCover(id);
            if (result.StatusCode == 200) path = result.Data.Path;
            return new HtmlContentViewComponentResult(
                new HtmlString($"<img src=\"{path}\" width=\"150\" height=\"150\" {html}>"));
        }
    }
}
