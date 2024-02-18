using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;
using Cactus.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Cactus.Controllers
{
    [Route("Author")]
    [AutoValidateAntiforgeryToken]
    public class AuthorController : Controller {
        private int PageSize = 4;
        private readonly IAuthorSubscribeService authorSubscribeService;
        private readonly IPostService postService;
        private readonly LinkGenerator linkGenerator;
        private readonly IAuthorService authorService;
        private readonly ICategoryService categoryService;
        private readonly IUninterestingAuthorService uninterestingAuthorService;
        public AuthorController(IAuthorSubscribeService authorSubscribeService, ICategoryService categoryService,
           IPostService postService, LinkGenerator linkGenerator, IAuthorService authorService,
           IUninterestingAuthorService uninterestingAuthorService) {
            this.authorSubscribeService = authorSubscribeService;
            this.postService = postService;
            this.linkGenerator = linkGenerator;
            this.authorService = authorService;
            this.categoryService = categoryService;
            this.uninterestingAuthorService = uninterestingAuthorService;
        }

        [Route("{UrlPage}")]
        [AllowAnonymous]
        public async Task<IActionResult> Index(string UrlPage, int authorPage = 1, int postPage = 1) {
            var response = new PagingAuthorViewModel();
            BaseResponse<User> author = await authorService.GetUserByUrlPageAsync(UrlPage);
            if (author.Data?.Id == Convert.ToInt32(User.FindFirstValue("Id"))) {
                response.IsOwner = true;
            }
            if (author.StatusCode == 200) {
                BaseResponse<UninterestingAuthor> uninterestings = await uninterestingAuthorService.GetAuthorAsync(Convert.ToInt32(User.FindFirstValue("Id")), author.Data.Id);
                response.IsUninteresting = true;
                
                BaseResponse<PagingAuthorViewModel> subs = await authorSubscribeService.GetUserViewSubscribersAsync(author.Data.Id, authorPage, PageSize);
                if (subs.StatusCode == 200) {
                    response.SubscribesPagingInfo = subs.Data.SubscribesPagingInfo;
                    response.Authors = subs.Data.Authors;
                }

                BaseResponse<PagingAuthorViewModel> posts = await postService.GetUserViewPostsAsync(author.Data.Id, postPage, PageSize);
                if (posts.StatusCode == 200) {
                    response.PostsPagingInfo = posts.Data.PostsPagingInfo;
                    response.Posts = posts.Data.Posts;
                }
                BaseResponse<IEnumerable<Category>> categories = await categoryService.GetAll();
                response.Categories = categories.Data;
            }
            response.CurrentUser = author.Data;
            return View(response);
        }

        [HttpPost]
        [Authorize(Roles = "Author")]
        public async Task<IActionResult> AddPost(PagingAuthorViewModel model) {
            await postService.AddPost(model.Post, Convert.ToInt32(User.FindFirstValue("Id")));
            BaseResponse<Author> response = await authorService.GetAsync(Convert.ToInt32(User.FindFirstValue("Id")));
            string path = "";
            if (response.StatusCode == 200)
                path = linkGenerator.GetPathByAction("Index", "Author", new { UrlPage = response.Data.UrlPage })!;
            return Redirect(path);
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> AddUninterestingAuthor(int authorId) {
            await uninterestingAuthorService.AddUninterestingAuthor(Convert.ToInt32(User.FindFirstValue("Id")), authorId);
            BaseResponse<Author> response = await authorService.GetAsync(authorId);
            string path = "";
            if (response.StatusCode == 200)
                path = linkGenerator.GetPathByAction("Index", "Author", new { UrlPage = response.Data.UrlPage })!;
            return Redirect(path);
        }
    }
}
