using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;
using Cactus.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;
using System.Security.Claims;

namespace Cactus.Pages
{
    public class PostModel : PageModel
    {
        private readonly IPostService postService;
        private readonly IPostMaterialService postMaterialService;
        private readonly IPostCommentService postCommentService;
        private readonly IPostTagService postTagService;
        private readonly IDonatorService donatorService;
        private readonly IPostDonationOptionService postDonationOptionService;
        public PostModel(IPostService postService, IPostMaterialService postMaterialService,
            IPostCommentService postCommentService, IPostTagService postTagService,
            IDonatorService donatorService, IPostDonationOptionService postDonationOptionService) {
            this.postService = postService;
            this.postMaterialService = postMaterialService;
            this.postCommentService = postCommentService;
            this.postTagService = postTagService;
            this.donatorService = donatorService;
            this.postDonationOptionService = postDonationOptionService;
        }

        public string ReturnUrl { get; set; }
        public Post Post { get; set; }
        public PostMaterial Material { get; set; }
        [BindProperty]
        public CommentViewModel PostComment { get; set; }
        public IEnumerable<PostComment> PostComments { get; set; }=new List<PostComment>();
        public IEnumerable<Tag> PostTags { get; set; }=new List<Tag>();
        public string CommentDescription {  get; set; }
        public NewComplainViewModel NewComplain { get; set; }
        public DonationOption DonationOption { get; set; }
        public Donator CurrentDonator { get; set; }
        public string PostAccessDescription { get; set; }
        public bool IsOwner { get; set; }

        public async Task<IActionResult> OnGetAsync(int postId)
        {
            BaseResponse<Post> post = await postService.GetPostByIdAsync(postId);
            if (post.StatusCode == 200) {
                Post = post.Data;
                if (post.Data.UserId == Convert.ToInt32(User.FindFirstValue("Id")))
                    IsOwner = true;

                BaseResponse<PostMaterial> material = await postMaterialService.GetPhotoAsync(Post.Id);
                if (material.StatusCode == 200) {
                    Material = material.Data;
                }

                BaseResponse<IEnumerable<PostComment>> comments = await postCommentService.GetComments(Post.Id);
                if (comments.StatusCode == 200) {
                    PostComments = comments.Data.ToList();
                }

                BaseResponse<IEnumerable<Tag>> tags = await postTagService.GetPostTagsAsync(post.Data.Id);
                if (tags.StatusCode == 200) {
                    PostTags = tags.Data;
                }

                BaseResponse<DonationOption> donationOption = await postDonationOptionService.GetOption(post.Data.Id);
                if (donationOption.StatusCode == 200) {
                    DonationOption = donationOption.Data;

                    BaseResponse<Donator> donator = await donatorService.GetDonator(post.Data.Id, (int)Models.Enums.DonationTargetType.Post, Convert.ToInt32(User.FindFirstValue("Id")));
                    if (donator.StatusCode == 200) {
                        CurrentDonator = donator.Data;
                    }
                    else PostAccessDescription = donator.Description;
                }
            }
            return Page ();
        }

        public async Task<IActionResult> OnPostAsync() {
            BaseResponse<PostComment> response =await postCommentService.Create(PostComment);
            CommentDescription = response.Description;
            return RedirectToPage("/Post", new {postId= PostComment.PostId});
        }
    }
}
