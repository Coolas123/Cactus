using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Services.Interfaces;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace Cactus.Components
{
    public class PostPhotoPath : ViewComponent
    {
        private readonly IPostMaterialService postMaterialService;
        public PostPhotoPath(IPostMaterialService postMaterialService) {
            this.postMaterialService = postMaterialService;
        }
        public async Task<IViewComponentResult> InvokeAsync(int id,string html="") {
            string path = "";
            BaseResponse<PostMaterial> result = await postMaterialService.GetPhotoAsync(id);
            if(result.StatusCode==200) path=result.Data.Path;
            return new HtmlContentViewComponentResult(
                new HtmlString($"<img src=\"{path}\" width=\"300\" height=\"450\" {html}>"));
        }
    }
}
