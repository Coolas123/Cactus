﻿using Cactus.Models.Database;

namespace Cactus.Infrastructure.Interfaces
{
    public interface IPostRepository:IBaseRepository<Post>
    {
        Task<IEnumerable<Post>> GetPagingPostsAsync(int authorId, int postPage, int pageSize);
        Task<IEnumerable<Post>> GetPostsAsync(int authorId);
        Task<Post> GetPostByIdAsync(int postId);
        Task<Post> GetLastAsync(DateTime time);
        Task<IEnumerable<Post>> GetPostsByTitleAsync(IEnumerable<string> titles);
        Task<Post> GetPostWithUserAsync(int postId);
    }
}
