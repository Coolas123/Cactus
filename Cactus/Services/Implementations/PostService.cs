using Cactus.Components;
using Cactus.Infrastructure.Interfaces;
using Cactus.Infrastructure.Repositories;
using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;
using Cactus.Services.Interfaces;
using SportsStore.Models;

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

        public async Task<BaseResponse<PagingAuthorViewModel>> GetUserViewPostsAsync(int userId, int postPage, int PageSize) {
            BaseResponse<IEnumerable<Post>> postList = await GetPagingPostsAsync(userId, postPage, PageSize);
            BaseResponse<IEnumerable<Post>> allPost = await GetPostsAsync(userId);

            var response = new PagingAuthorViewModel();
            if (postList.StatusCode == 200) {
                response.Posts = postList.Data;
                response.PostsPagingInfo = new PagingInfo
                {
                    CurrentPage = postPage,
                    ItemsPerPage = PageSize,
                    TotalItems = allPost.Data.Count()
                };
            }
            else {
                response.PostsPagingInfo = new PagingInfo
                {
                    Description = postList.Description
                };
            }
            return new BaseResponse<PagingAuthorViewModel> { 
                Data = response,
                StatusCode=200
            };
        }
    }
}
