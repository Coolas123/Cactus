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
        private readonly IPostCategoryService postCategoryService;
        private readonly ITagService tagService;
        private readonly IPostTagService postTagService;
        public PostService(IPostRepository postRepository, IPostMaterialService postMaterialService,
            IPostCategoryService postCategoryService, ITagService tagService,
            IPostTagService postTagService) {
            this.postRepository = postRepository;
            this.postMaterialService = postMaterialService;
            this.postCategoryService = postCategoryService;
            this.tagService = tagService;
            this.postTagService = postTagService;
        }
        public async Task<BaseResponse<Post>> AddPost(PostViewModel model, int id) {
            var post = new Post
            {
                UserId = id,
                Title = model.Title,
                Description = model.Description,
                Created= (DateTime)model.Created
            };
            await postRepository.CreateAsync(post);
            Post lastPost = await postRepository.GetLastAsync((DateTime)model.Created);
            await postCategoryService.CreateAsync(lastPost.Id,model.CategoryId);
            if (model.PostPhoto != null)
                await postMaterialService.AddPhotoAsync(model.PostPhoto, lastPost.Id);

            return new BaseResponse<Post>
            {
                Data = post,
                StatusCode = 200
            };
        }

        public async Task<BaseResponse<Post>> GetLastAsync(DateTime created) {
            Post lastPost = await postRepository.GetLastAsync(created);
            if (lastPost == null) {
                return new BaseResponse<Post>
                {
                    Description="Пост не найден"
                };
            }
            return new BaseResponse<Post>
            {
                Data= lastPost,
                StatusCode=200
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

        public async Task<BaseResponse<Post>> GetPostByIdAsync(int postId) {
            Post post = await postRepository.GetPostByIdAsync(postId);
            if (post == null) {
                return new BaseResponse<Post>
                {
                    Description="Пост не найден"
                };
            }
            return new BaseResponse<Post>
            {
                Data = await postRepository.GetPostByIdAsync(postId),
                StatusCode = 200
            };
        }

        public async Task<BaseResponse<IEnumerable<Post>>> GetPostsAsync(int authorId) {
            IEnumerable<Post> posts = await postRepository.GetPostsAsync(authorId);
            if (posts == null) {
                return new BaseResponse<IEnumerable<Post>>
                {
                    Description = "Посты не найдены"
                };
            }
            return new BaseResponse<IEnumerable<Post>>
            {
                Data = await postRepository.GetPostsAsync(authorId),
                StatusCode = 200
            };
        }

        public async Task<BaseResponse<IEnumerable<Post>>> GetPostsByTagsAsync(IEnumerable<string> tags) {
            BaseResponse<IEnumerable<Tag>> dbTags = await tagService.GetAllByNames(tags);
            if (dbTags.StatusCode==200) {
                BaseResponse<IEnumerable<Post>> postTags= await postTagService.GetPostsByTagsAsync(dbTags.Data);
                if (postTags.StatusCode==200) {
                    return new BaseResponse<IEnumerable<Post>>
                    {
                        Data=postTags.Data,
                        StatusCode=200
                    };
                }
            }
            return new BaseResponse<IEnumerable<Post>>
            {
                Description=dbTags.Description
            };
        }

        public async Task<BaseResponse<IEnumerable<Post>>> GetPostsByTitleAsync(IEnumerable<string> titles) {
            IEnumerable<Post> posts = await postRepository.GetPostsByTitleAsync(titles);
            if (posts.Any()) {
                return new BaseResponse<IEnumerable<Post>>
                {
                    Data = posts,
                    StatusCode = 200
                };
            }
            return new BaseResponse<IEnumerable<Post>>
            {
                Description="Посты не найдены"
            };
        }

        public async Task<BaseResponse<PagingAuthorViewModel>> GetUserViewPostsAsync(int userId, int postPage, int PageSize) {
            BaseResponse<IEnumerable<Post>> postList = await GetPagingPostsAsync(userId, postPage, PageSize);
            BaseResponse<IEnumerable<Post>> allPost = await GetPostsAsync(userId);

            var response = new PagingAuthorViewModel();
            if (postList.StatusCode == 200) {
                response.Posts = postList.Data.OrderBy(x => x.Created);
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
