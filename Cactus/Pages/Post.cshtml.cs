using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace Cactus.Pages
{
    public class PostModel : PageModel
    {
        private readonly IPostService postService;
        private readonly IPostMaterialService postMaterialService;
        public PostModel(IPostService postService, IPostMaterialService postMaterialService) {
            this.postService = postService;
            this.postMaterialService = postMaterialService;
        }

        public string ReturnUrl { get; set; }
        public Post Post { get; set; }
        public PostMaterial Material { get; set; }

        public async Task<IActionResult> OnGetAsync(int postId)
        {
            BaseResponse<Post> post = await postService.GetPostByIdAsync(postId);
            if (post.StatusCode == 200) {
                Post = post.Data;
            }
            BaseResponse<PostMaterial> material = await postMaterialService.GetPhotoAsync(Convert.ToInt32(User.FindFirstValue("Id")));
            if (material.StatusCode == 200) {
                Material = material.Data;
            }
            return Page ();
        }
    }
}
