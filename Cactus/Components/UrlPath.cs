using Cactus.Infrastructure.Repositories;
using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;
using Cactus.Services.Implementations;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc;
using Cactus.Infrastructure.Interfaces;
using Cactus.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Routing;

namespace Cactus.Components
{
    public class UrlPath:ViewComponent
    {
        private readonly IMemoryCache cache;
        private readonly IIndividualRepository individualRepository;
        private readonly LinkGenerator linkGenerator;
        public UrlPath(IMemoryCache cache, IIndividualRepository individualRepository,
            LinkGenerator linkGenerator) {
            this.cache = cache;
            this.individualRepository = individualRepository;
            this.linkGenerator = linkGenerator;
        }
        public async Task<IViewComponentResult> InvokeAsync(int id) {
            IndividualProfileViewModel profile;
            cache.TryGetValue("IndividualProfile", out profile);
            if (profile == null) {
                profile = new IndividualProfileViewModel();
            }
            if (profile.UrlPage == null) {
                Individual individual = await individualRepository.GetAsync(id);
                profile.UrlPage = individual.UrlPage;
                cache.Set("IndividualProfile", profile);
            }
            var path= linkGenerator.GetPathByAction(
                "Index", "Individual", new { UrlPage= profile.UrlPage })!;
            return new HtmlContentViewComponentResult(
                new HtmlString($"<li><a class=\"dropdown-item\" href={path}>Профиль</a></li>"));
        }
    }
}
