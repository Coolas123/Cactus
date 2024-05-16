using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Services.Interfaces;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace Cactus.Components
{
    public class Complain:ViewComponent
    {
        private readonly IComplainService complainService;
        private readonly IUserService userService;
        private readonly IPostCommentService postCommentService;
        private readonly IPostService postService;
        public Complain(IComplainService complainService, IUserService userService,
            IPostCommentService postCommentService, IPostService postService) {
            this.complainService = complainService;
            this.userService = userService;
            this.postCommentService = postCommentService;
            this.postService = postService;
        }
        public async Task<IViewComponentResult> InvokeAsync(int targetId, int targetTypeId) {
            string response = "";
            switch (targetTypeId) {
                case (int)Models.Enums.ComplainTargetType.Post:
                    BaseResponse<Post> post = await postService.GetPostByIdAsync(targetId);
                    response = $"<a class=\"form-control\" href=\"/Post/{post.Data.Id}\">Пост</a>";
                    break;
                case (int)Models.Enums.ComplainTargetType.User:
                    BaseResponse<User> user = await userService.GetAsync(targetId);
                    if(user.Data.UserRoleId == (int)Models.Enums.UserRole.Patron) {
                        response = $"<a class=\"form-control\" href=\"/Patron/{user.Data.Id}\">Пользователь</a>";
                    }
                    else if (user.Data.UserRoleId == (int)Models.Enums.UserRole.Author) {
                        response = $"<a class=\"form-control\" href=\"/Author/{user.Data.Id}\">Пользователь</a>";
                    }
                    break;
                case (int)Models.Enums.ComplainTargetType.Comment:
                    BaseResponse<PostComment> comment = await postCommentService.GetComment(targetId);
                    response = $"<a class=\"form-control\" href=\"/Post/{comment.Data.PostId}\">Пост</a>\n" +
                        $"<h4>Содержание комментария</h4>" +
                        $"<textarea class=\"form-control\" readonly style=\"resize:none; width: 100%;\">{comment.Data.Comment}</textarea>";
                    break;
            }
            return new HtmlContentViewComponentResult(
                new HtmlString(response));
        }
    }
}
