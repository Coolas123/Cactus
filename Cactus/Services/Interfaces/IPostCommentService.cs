﻿using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;

namespace Cactus.Services.Interfaces
{
    public interface IPostCommentService
    {
        Task<BaseResponse<PostComment>> Create(CommentViewModel model);
        Task<BaseResponse<IEnumerable<PostComment>>> GetComments(int postId);
        Task<BaseResponse<PostComment>> GetComment(int postId);
    }
}
