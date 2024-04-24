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
        private readonly IDonationOptionService donationOptionService;
        private readonly IDonatorService donatorService;
        private readonly IPaidAuthorSubscribeService paidAuthorSubscribeService;
        public AuthorController(IAuthorSubscribeService authorSubscribeService, ICategoryService categoryService,
           LinkGenerator linkGenerator, IAuthorService authorService, IPostService postService,
           IUninterestingAuthorService uninterestingAuthorService,IDonationOptionService donationOptionService,
           IDonatorService donatorService, IPaidAuthorSubscribeService paidAuthorSubscribeService) {
            this.authorSubscribeService = authorSubscribeService;
            this.postService = postService;
            this.linkGenerator = linkGenerator;
            this.authorService = authorService;
            this.categoryService = categoryService;
            this.uninterestingAuthorService = uninterestingAuthorService;
            this.donationOptionService = donationOptionService;
            this.donatorService = donatorService;
            this.paidAuthorSubscribeService = paidAuthorSubscribeService;
        }

        [Route("{UrlPage}")]
        [Route("/id/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Index(string UrlPage="/", int id = 0, int authorPage = 1, int postPage = 1, bool NotEnoughBalance = false, bool postCreated=false, bool isPaidSubscribed=false, bool isPaidGoal=false, bool isRemittance= false) {
            var response = new PagingAuthorViewModel();
            BaseResponse<User> author;
            if (id == 0) {
                author = await authorService.GetUserByUrlPageAsync(UrlPage);
            }
            else author = await authorService.GetUserAsync(id);
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
            }
            BaseResponse<IEnumerable<DonationOption>> options = await donationOptionService.GetOptionsAsync(author.Data.Id);
            if (options.StatusCode == 200) {
                response.DonationOptions = options.Data.OrderBy(x=>x.Price);
                List<int> goals = options.Data.Where(x => x.MonetizationTypeId == (int)Models.Enums.MonetizationType.Goal).Select(x => x.Id).ToList();
                BaseResponse<Dictionary<int, decimal>> donators = await donatorService.GetCollectedSumOfGoals(goals);
                if (donators.StatusCode == 200) {
                    response.CollectedGoal = donators.Data;
                }
            }
            BaseResponse<IEnumerable<PaidAuthorSubscribe>> paidSubs = await paidAuthorSubscribeService.GetCurrentSubscribes(Convert.ToInt32(User.FindFirstValue("Id")));
            if (paidSubs.StatusCode == 200) {
                response.PaidSubscribes= paidSubs.Data;
            }
            response.CurrentUser = author.Data;
            response.NotEnoughBalance = NotEnoughBalance;
            response.PostCreated = postCreated;
            response.isPaidSubscribed = isPaidSubscribed;
            response.IsPaidGoal = isPaidGoal;
            response.IsRemittance = isRemittance;
            return View(response);
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

        [HttpGet("RemoveUninterestingAuthor")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> RemoveUninterestingAuthor(int authorId) {
            BaseResponse<bool> response = await uninterestingAuthorService.RemoveUninterestingAuthor(Convert.ToInt32(User.FindFirstValue("Id")), authorId);
            BaseResponse<Author> responseAuthor = await authorService.GetAsync(authorId);
            return RedirectToAction("Index", new { responseAuthor.Data.UrlPage });
        }

        [HttpGet("/unsubscribe")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> UnsubscribeToAuthor(int authorId) {
            BaseResponse<AuthorSubscribe> unsub = await authorSubscribeService.UnsubscribeToAuthor(Convert.ToInt32(User.FindFirstValue("Id")), authorId);
            BaseResponse<Author> response = await authorService.GetAsync(authorId);
            string path = "";
            if (response.StatusCode == 200)
                path = linkGenerator.GetPathByAction("Index", "Author", new { UrlPage = response.Data.UrlPage })!;
            return Redirect(path);
        }

    }
}
