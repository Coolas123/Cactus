﻿using Cactus.Infrastructure.Interfaces;
using Cactus.Infrastructure.Repositories;
using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;
using Cactus.Services.Interfaces;

namespace Cactus.Services.Implementations
{
    public class PostService : IPostService
    {
        private readonly IPostRepository postRepository;
        private readonly IPostMaterialService postMaterialService;
        public PostService(IPostRepository postRepository, IPostMaterialService postMaterialService) {
            this.postRepository = postRepository;
            this.postMaterialService = postMaterialService;
        }
        public async Task<BaseResponse<Post>> AddPost(PostViewModel model, int id) {
            var post = new Post
            {
                UserId = id,
                Title = model.Title,
                Description = model.Description
            };
            await postRepository.CreateAsync(post);
            if (model.PostPhoto != null)
                await postMaterialService.AddPhotoAsync(model.PostPhoto, id);
            return new BaseResponse<Post>
            {
                Data = post,
                StatusCode = 200
            };
        }

        public async Task<BaseResponse<IEnumerable<Post>>> GetPagingPostsAsync(int authorId, int postPage, int pageSize) {
            IEnumerable<Post> posts = await postRepository.GetPagingPostsAsync(authorId, postPage, pageSize);
            if (posts.Count() == 0) {
                return new BaseResponse<IEnumerable<Post>>
                {
                    Description = "Посты отсутствуют"
                };
            }
            return new BaseResponse<IEnumerable<Post>>
            {
                Data = posts,
                StatusCode = 200
            };
        }

        public async Task<BaseResponse<IEnumerable<Post>>> GetPostsAsync(int authorId) {
            return new BaseResponse<IEnumerable<Post>>
            {
                Data = await postRepository.GetPostsAsync(authorId)
            };
        }
    }
}
