using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;
using Cactus.Services.Interfaces;

namespace Cactus.Services.Implementations
{
    public class NewsFeedService : INewsFeedService
    {
        private readonly IPostService postService;
        private readonly IPostTagService postTagService;
        private readonly IAuthorService authorService;
        public NewsFeedService(IPostService postService, IPostTagService postTagService,
            IAuthorService authorService) {
            this.postService = postService;
            this.postTagService = postTagService;
            this.authorService = authorService;
        }
        public async Task<BaseResponse<SearchResultViewModel>> Search(SearchViewModel model) {
            var searchs = model.SearchText?.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            List<string> tags = new List<string>();
            List<string> str = new List<string>();
            for (int i = 0; i < searchs?.Length; i++) {
                if (searchs[i][0] == '#') {
                    tags.Add(searchs[i].Remove(0, 1));
                    if (tags.Last().Contains('#')) {
                        var temp = tags.Last();
                        tags.AddRange(tags.Last().Split('#', StringSplitOptions.RemoveEmptyEntries));
                        tags.Remove(temp);
                    }
                }
                else {
                    str.Add(searchs[i]);
                }
            }

            var response = new BaseResponse<SearchResultViewModel>
            {
                Data = new SearchResultViewModel
                {
                    Posts=new List<Post>()
                }
            };

            if (model.IsPost) {
                var posts = new List<Post>();
                var postsByTag = await postService.GetPostsByTagsAsync(tags);
                if (postsByTag.StatusCode == 200) {
                    posts.AddRange(postsByTag.Data.OrderBy(x=>x.Created));
                }
                var postsByTitle = await postService.GetPostsByTitleAsync(str);
                if (postsByTitle.StatusCode == 200) {
                    posts.AddRange(postsByTitle.Data.OrderBy(x => x.Created));
                }
                response.Data.Posts = posts;
            }

            if(model.IsAuthor) {
                var authors = new List<Author>();
                var authorByTag = await postTagService.GetAuthorsByTagsAsync(tags);
                if (authorByTag.StatusCode == 200) {
                    authors.AddRange(authorByTag.Data);
                }
                var authorByName = await authorService.GetUserByNameAsync(str);
                if (authorByName.StatusCode == 200) {
                    authors.Add(authorByName.Data);
                }
                response.Data.Authors=authors;
            }

            if(response.Data.Authors!=null|| response.Data.Posts != null) {
                response.StatusCode = 200;
                return response;
            }
            return new BaseResponse<SearchResultViewModel>
            {
                Description="Ничего не найдено"
            };
        }
    }
}
