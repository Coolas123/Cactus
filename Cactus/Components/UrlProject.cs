using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Services.Interfaces;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace Cactus.Components
{
    public class UrlProject:ViewComponent
    {
        private readonly IProjectService projectService;
        private readonly LinkGenerator linkGenerator;
        public UrlProject(IProjectService projectService, LinkGenerator linkGenerator) {
            this.projectService = projectService;
            this.linkGenerator = linkGenerator;
        }
        public IViewComponentResult Invoke(Project project, string html = "") {
            string path = "";
            if (project!=null)
                path = linkGenerator.GetPathByAction(
                    "Index", "Project", new { UrlPage = project.UrlProject })!;
            return new HtmlContentViewComponentResult(
                new HtmlString($"<a {html} href={path}>Профиль</a>"));
        }
    }
}
