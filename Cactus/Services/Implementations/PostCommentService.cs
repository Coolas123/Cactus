using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;
using Cactus.Services.Interfaces;

namespace Cactus.Services.Implementations
{
    public class PostCommentService : IPostCommentService
    {
        private readonly IPostCommentRepository postCommentRepository;
        public PostCommentService(IPostCommentRepository postCommentRepository) {
            this.postCommentRepository = postCommentRepository;
        }
        public async Task<BaseResponse<PostComment>> Create(CommentViewModel model) {
            PostComment postComment = new PostComment
            {
                Comment=model.Comment,
                PostId = model.PostId,
                UserId = model.UserId,
                Created=model.Created.ToUniversalTime()
            };
            var res = await postCommentRepository.CreateAsync(postComment);
            if (!res) {
                return new BaseResponse<PostComment>
                {
                    Description = "Не удалось оставить комментарий"
                };
            }
            return new BaseResponse<PostComment>
            {
                Data= postComment,
                StatusCode =200,
                Description="Комментарий оставлен"
            };
        }

        public async Task<BaseResponse<IEnumerable<PostComment>>> GetComments(int postId) {
            IEnumerable<PostComment> comments = await postCommentRepository.GetCommentsAsync(postId);
            if (comments.Count() == 0) {
                return new BaseResponse<IEnumerable<PostComment>>{
                    Description="Комментариев еще нет"
                };
            }
            return new BaseResponse<IEnumerable<PostComment>>
            {
                Data=comments,
                StatusCode =200
            };
        }
    }
}
