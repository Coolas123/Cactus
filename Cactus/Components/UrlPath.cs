using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.Caching.Memory;

namespace Cactus.Components
{
    public class UrlPath:ViewComponent
    {
        private readonly IMemoryCache cache;
        private readonly IAuthorRepository authorRepository;
        private readonly LinkGenerator linkGenerator;
        public UrlPath(IMemoryCache cache, IAuthorRepository authorRepository,
            LinkGenerator linkGenerator) {
            this.cache = cache;
            this.authorRepository = authorRepository;
            this.linkGenerator = linkGenerator;
        }
        public async Task<IViewComponentResult> InvokeAsync(int id,string html="") {
            string path = "";
            Author Author = await authorRepository.GetAsync(id);
            if (Author != null)
                path = linkGenerator.GetPathByAction(
                    "Index", "Author", new { UrlPage = Author.UrlPage })!;
            return new HtmlContentViewComponentResult(
                new HtmlString($"<a {html} href={path}>Профиль</a>"));
        }
    }
}
