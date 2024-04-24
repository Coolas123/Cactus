using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;
using Cactus.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Drawing;
using System.Security.Claims;

namespace Cactus.Controllers
{
    [Authorize(Roles = "Author")]
    public class PostController:Controller
    {
        private readonly IPostService postService;
        private readonly IPostTagService postTagService;
        private readonly IPostDonationOptionService postDonationOptionService;
        private readonly IDonationOptionService donationOptionService;
        private readonly IAuthorService authorService;
        private readonly LinkGenerator linkGenerator;
        private readonly ICategoryService categoryService;
        public PostController(IPostService postService, IPostTagService postTagService,
            IPostDonationOptionService postDonationOptionService, IDonationOptionService donationOptionService
            , IAuthorService authorService, LinkGenerator linkGenerator, ICategoryService categoryService) {
            this.postService = postService;
            this.postTagService = postTagService;
            this.postDonationOptionService = postDonationOptionService;
            this.donationOptionService = donationOptionService;
            this.authorService = authorService;
            this.linkGenerator = linkGenerator;
            this.categoryService = categoryService;
        }

        public async Task<IActionResult> Index() {
            var response = new NewPostViewModel();
            BaseResponse<IEnumerable<DonationOption>> options = await donationOptionService.GetOptionsAsync(Convert.ToInt32(User.FindFirstValue("Id")));
            if (options.StatusCode == 200) {
                response.DonationOptions = options.Data;
                List<int> goals = options.Data.Where(x => x.MonetizationTypeId == (int)Models.Enums.MonetizationType.Goal).Select(x => x.Id).ToList();
            }
            BaseResponse<IEnumerable<Category>> categories = await categoryService.GetAll();
            response.Categories = categories.Data;
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddPost(NewPostViewModel model) {
            bool postCreated = false;
            if (model.Post.PostPhoto != null) {
                var image = Image.FromStream(model.Post.PostPhoto.OpenReadStream());
                if (image.Width > 590 || image.Height > 450) {
                    ModelState.AddModelError("Post.PostPhoto", "Изображение должно быть не более чем 590px на 450px");
                }
            }
            if (ModelState["NewDonationOption.Price"].Errors.Count != 0) {
                ModelState["NewDonationOption.Price"].Errors.Clear();
                ModelState.AddModelError("NewDonationOption.Price", "Неверная цена монетизации");
            }
            foreach (var state in ModelState) {
                if (state.Key.Split('.').Contains("Post") &&
                    state.Value.Errors.Count != 0) {
                    BaseResponse<IEnumerable<DonationOption>> options = await donationOptionService.GetOptionsAsync(Convert.ToInt32(User.FindFirstValue("Id")));
                    if (options.StatusCode == 200) {
                        model.DonationOptions = options.Data;
                        List<int> goals = options.Data.Where(x => x.MonetizationTypeId == (int)Models.Enums.MonetizationType.Goal).Select(x => x.Id).ToList();
                    }
                    BaseResponse<IEnumerable<Category>> categories = await categoryService.GetAll();
                    model.Categories = categories.Data;
                    if (model.Post.IsFree) {
                        foreach (var s in ModelState) {
                            if (s.Key.Split('.').Contains("NewDonationOption") &&
                                s.Value.Errors.Count != 0) {
                                ModelState.Remove(s.Key);
                            }
                        }
                    }
                    return View("Index", model);
                }
            }
            if (!model.Post.IsFree && model.SelectedDonationOption==0) {
                foreach (var state in ModelState) {
                    if (state.Key.Split('.').Contains("NewDonationOption") &&
                        state.Value.Errors.Count != 0) {
                        BaseResponse<IEnumerable<DonationOption>> options = await donationOptionService.GetOptionsAsync(Convert.ToInt32(User.FindFirstValue("Id")));
                        if (options.StatusCode == 200) {
                            model.DonationOptions = options.Data;
                            List<int> goals = options.Data.Where(x => x.MonetizationTypeId == (int)Models.Enums.MonetizationType.Goal).Select(x => x.Id).ToList();
                        }
                        BaseResponse<IEnumerable<Category>> categories = await categoryService.GetAll();
                        model.Categories = categories.Data;
                        return View("Index", model);
                    }
                }
            }

            model.Post.Created = DateTime.Now.ToUniversalTime();
            var res = await postService.AddPost(model.Post, Convert.ToInt32(User.FindFirstValue("Id")));
            if (res.StatusCode == 200) {
                postCreated=true;
            }

            var tags = model.Post.Tags?.Split('#').Where(x => x != "").ToList();
            BaseResponse<Post> post = await postService.GetLastAsync((DateTime)model.Post.Created);
            if (tags is not null)
                await postTagService.AddTagsToPost(post.Data.Id, tags);

            if (model.SelectedDonationOption == 0) {
                if (!model.Post.IsFree) {
                    await donationOptionService.AddOptionAsync(model.NewDonationOption);
                    BaseResponse<DonationOption> dbOption = await donationOptionService.GetByPriceAsync(model.NewDonationOption.Price);
                    await postDonationOptionService.AddOptionToPostAsync(post.Data.Id, dbOption.Data.Id);
                }
            }
            else {
                await postDonationOptionService.AddOptionToPostAsync(post.Data.Id, (int)model.SelectedDonationOption);
            }


            BaseResponse<Author> response = await authorService.GetAsync(Convert.ToInt32(User.FindFirstValue("Id")));
            string path = "";
            if (response.StatusCode == 200)
                path = linkGenerator.GetPathByAction("Index", "Author", new { UrlPage = response.Data.UrlPage, postCreated= postCreated })!;
            return Redirect(path);
        }
    }
}
