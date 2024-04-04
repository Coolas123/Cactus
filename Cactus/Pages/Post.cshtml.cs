using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;
using Cactus.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using System.Threading;

namespace Cactus.Pages
{
    [Authorize(Roles = "Author, Patron")]
    [AutoValidateAntiforgeryToken]
    public class PostModel : PageModel
    {
        private readonly IPostService postService;
        private readonly IPostMaterialService postMaterialService;
        private readonly IPostCommentService postCommentService;
        private readonly IPostTagService postTagService;
        private readonly IDonatorService donatorService;
        private readonly IPostDonationOptionService postDonationOptionService;
        private readonly IPayMethodSettingService payMethodSettingService;
        private readonly ITransactionService transactionService;
        private readonly IWalletService walletService;
        private readonly IPostOneTimePurschaseDonatorService postOneTimePurschaseDonatorService;
        public PostModel(IPostService postService, IPostMaterialService postMaterialService,
            IPostCommentService postCommentService, IPostTagService postTagService,
            IDonatorService donatorService, IPostDonationOptionService postDonationOptionService,
            IPayMethodSettingService payMethodSettingService, ITransactionService transactionService,
            IWalletService walletService, IPostOneTimePurschaseDonatorService postOneTimePurschaseDonatorService) {
            this.postService = postService;
            this.postMaterialService = postMaterialService;
            this.postCommentService = postCommentService;
            this.postTagService = postTagService;
            this.donatorService = donatorService;
            this.postDonationOptionService = postDonationOptionService;
            this.payMethodSettingService = payMethodSettingService;
            this.transactionService = transactionService;
            this.walletService = walletService;
            this.postOneTimePurschaseDonatorService = postOneTimePurschaseDonatorService;
        }

        public string ReturnUrl { get; set; }
        [BindProperty]
        public Post Post { get; set; }
        public PostMaterial Material { get; set; }
        public CommentViewModel PostComment { get; set; }
        public IEnumerable<PostComment> PostComments { get; set; }=new List<PostComment>();
        public IEnumerable<Tag> PostTags { get; set; }=new List<Tag>();
        public string CommentDescription {  get; set; }
        public NewComplainViewModel NewComplain { get; set; }
        public DonationOption DonationOption { get; set; }
        public IEnumerable<Donator> CurrentDonator { get; set; }
        public string PostAccessDescription { get; set; }
        public bool IsOwner { get; set; }
        [BindProperty]
        public TransactionViewModel NewOneTimePurschase { get; set; }
        public bool NotEnoughBalance { get; set; } = false;

        public async Task<IActionResult> OnGetAsync(int postId, bool notEnoughBalance = false)
        {
            if (notEnoughBalance) NotEnoughBalance = true;
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

                    if (donationOption.Data.MonetizationTypeId == (int)Models.Enums.MonetizationType.OneTimePurchase) {
                        BaseResponse<PostOneTimePurschaseDonator> Postdonator = await postOneTimePurschaseDonatorService.GetDonator(Post.Id, Convert.ToInt32(User.FindFirstValue("Id")));
                        if (Postdonator.StatusCode == 200) {
                            CurrentDonator = new List<Donator> { Postdonator.Data.Donator };
                        }
                        else PostAccessDescription = Postdonator.Description;
                    }
                    else {
                        BaseResponse<IEnumerable<Donator>> donator = await donatorService.GetPostDonator(post.Data.Id, DonationOption.Id, Convert.ToInt32(User.FindFirstValue("Id")));
                        if (donator.StatusCode == 200) {
                            CurrentDonator = donator.Data;
                        }
                        else PostAccessDescription = donator.Description;
                    }
                }
            }
            return Page ();
        }

        public async Task<IActionResult> OnPostAsync() {
            BaseResponse<PostComment> response =await postCommentService.Create(PostComment);
            CommentDescription = response.Description;
            return RedirectToPage("/Post", new {postId= PostComment.PostId});
        }

        public async Task<IActionResult> OnPostOneTimePurschase() {
            BaseResponse<PayMethodSetting> setting = await payMethodSettingService.GetIntrasystemOperationsSetting();
            NewOneTimePurschase.Created = DateTime.Now;
            NewOneTimePurschase.PayMethodId = setting.Data.Id;
            NewOneTimePurschase.Received = NewOneTimePurschase.Sended - NewOneTimePurschase.Sended / 100 * setting.Data.Comission;
            NewOneTimePurschase.StatusId = (int)Models.Enums.TransactionStatus.Sended;
            NewOneTimePurschase.UserId = Convert.ToInt32(User.FindFirstValue("Id"));

            BaseResponse<Wallet> walletResponse = await walletService.WithdrawWallet(Convert.ToInt32(User.FindFirstValue("Id")), NewOneTimePurschase.Sended);
            if (walletResponse.StatusCode != 200) {
                NotEnoughBalance = false;
                return RedirectToPage("/Post", new { postId = NewOneTimePurschase.PostId, NotEnoughBalance = true});
            }

            await transactionService.CreateTransaction(NewOneTimePurschase);
            await walletService.ReplenishWallet(NewOneTimePurschase.AuthorId, NewOneTimePurschase.Received);

            BaseResponse<Transaction> newTransaction = await transactionService.GetLastTransaction(Convert.ToInt32(User.FindFirstValue("Id")), NewOneTimePurschase.Created);
            var donatorViewModel = new DonatorViewModel
            {
                UserId = NewOneTimePurschase.UserId,
                DonationOptionId = NewOneTimePurschase.DonationOptionId,
                DonationTargetTypeId = (int)Models.Enums.DonationTargetType.Author,
                TransactionId = newTransaction.Data.Id
            };
            await donatorService.AddDonator(donatorViewModel);
            BaseResponse<Donator> lastDonator = await donatorService.GetLastDonator(NewOneTimePurschase.Created.ToUniversalTime(), Convert.ToInt32(User.FindFirstValue("Id")));
            await postOneTimePurschaseDonatorService.AddDonator(NewOneTimePurschase.PostId, lastDonator.Data.Id);
            return RedirectToPage("/Post", new { postId = NewOneTimePurschase.PostId });
        }
    }
}
