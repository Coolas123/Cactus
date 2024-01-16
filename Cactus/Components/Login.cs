using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Services.Interfaces;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.Caching.Memory;

namespace Cactus.Components
{
    public class Login: ViewComponent
    {
        private readonly IMaterialService materialService;
        private readonly IMemoryCache cache;
        public Login(IMaterialService materialService, IMemoryCache cache) {
            this.materialService = materialService;
            this.cache = cache;
        }
        public async Task<IViewComponentResult> InvokeAsync(int userId, string html="") {
            string avatarPath = "";
            if (!cache.TryGetValue("AvatarPath", out avatarPath)) {
                BaseResponse<Material> result = await materialService.GetAvatarAsync(userId);
                if (result.StatusCode == StatusCodes.Status200OK) {
                    avatarPath = result.Data.Path;
                    cache.Set("AvatarPath", avatarPath,
                        new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(1)));
                }
            }
            return new HtmlContentViewComponentResult(
                new HtmlString($"<img src=\"{avatarPath}\" alt=\"Аватарка\" class=\"avatar\" width=\"50\" height=\"50\" {html}>"));
        }
    }
}
