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
        private readonly IPostTagService postTagService;
        private readonly IDonationOptionService donationOptionService;
        private readonly IPostDonationOptionService postDonationOptionService;
        public AuthorController(IAuthorSubscribeService authorSubscribeService, ICategoryService categoryService,
           IPostService postService, LinkGenerator linkGenerator, IAuthorService authorService,
           IUninterestingAuthorService uninterestingAuthorService, IPostTagService postTagService, IDonationOptionService donationOptionService,
           IPostDonationOptionService postDonationOptionService) {
            this.authorSubscribeService = authorSubscribeService;
            this.postService = postService;
            this.linkGenerator = linkGenerator;
            this.authorService = authorService;
            this.categoryService = categoryService;
            this.uninterestingAuthorService = uninterestingAuthorService;
            this.postTagService = postTagService;
            this.donationOptionService = donationOptionService;
            this.postDonationOptionService = postDonationOptionService;
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
                if(uninterestings.StatusCode==200)
                    response.IsUninteresting = true;
                BaseResponse<AuthorSubscribe> sub = await authorSubscribeService.GetSubscribe(Convert.ToInt32(User.FindFirstValue("Id")),author.Data.Id);
                if (sub.StatusCode == 200) {
                    response.IsSubscribe = true;
                }

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
                BaseResponse<IEnumerable<DonationOption>> options = await donationOptionService.GetOptionsAsync(author.Data.Id);
                if (options.StatusCode == 200) {
                    response.DonationOptions = options.Data;
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
            model.Post.Created=DateTime.Now.ToUniversalTime();
            await postService.AddPost(model.Post, Convert.ToInt32(User.FindFirstValue("Id")));
            
            var tags = model.Post.Tags?.Split('#').Where(x => x != "").ToList();
            BaseResponse<Post> post =await postService.GetLastAsync(model.Post.Created);
            if(tags is not null)
                await postTagService.AddTagsToPost(post.Data.Id, tags);

            if (model.SelectedDonationOption == 0) {
                await donationOptionService.AddOptionAsync(model.NewDonationOption);
            }
            else {
                await postDonationOptionService.AddOptionToPostAsync(post.Data.Id,model.SelectedDonationOption);
            }

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

        [HttpGet("/subscribe")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> SubscribeToAuthor(int authorId) {
            await authorSubscribeService.SubscribeToAuthor(Convert.ToInt32(User.FindFirstValue("Id")),authorId);
            BaseResponse<Author> response = await authorService.GetAsync(authorId);
            string path = "";
            if (response.StatusCode == 200)
                path = linkGenerator.GetPathByAction("Index", "Author", new { UrlPage = response.Data.UrlPage })!;
            return Redirect(path);
        }
    }
}
