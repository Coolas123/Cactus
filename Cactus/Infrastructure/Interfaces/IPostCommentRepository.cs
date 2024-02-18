﻿using Cactus.Models.Database;

namespace Cactus.Infrastructure.Interfaces
{
    public interface IPostCommentRepository : IBaseRepository<PostComment>
    {
        Task<IEnumerable<PostComment>> GetComments(int postId);
    }
}