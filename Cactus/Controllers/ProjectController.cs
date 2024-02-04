using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;
using Cactus.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Cactus.Controllers
{
    [Route("Project")]
    [AutoValidateAntiforgeryToken]
    public class ProjectController : Controller
    {
        private int PageSize = 10;
        private readonly IProjectService projectService;
        public ProjectController(IProjectService projectService) {
            this.projectService = projectService;
        }

        [AllowAnonymous]
        public IActionResult Index() {

            return View();
        }

        [Authorize(Roles = "Legal")]
        [HttpGet]
        public async Task<IActionResult> ProjectList() {
            var response = new PagingProjectViewModel();
            BaseResponse<PagingProjectViewModel> projects = await projectService
                .GetUserViewProjectsAsync(Convert.ToInt32(User.FindFirstValue("Id")), PageSize, PageSize);
            if (projects.StatusCode == 200) {
                response.ProjectsPagingInfo = projects.Data.ProjectsPagingInfo;
                response.Projects = projects.Data.Projects;
            }
            return View(response);
        }
    }
}
