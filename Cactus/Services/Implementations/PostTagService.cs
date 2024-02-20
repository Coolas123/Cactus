using Cactus.Infrastructure.Interfaces;
using Cactus.Infrastructure.Repositories;
using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Services.Interfaces;

namespace Cactus.Services.Implementations
{
    public class PostTagService : IPostTagService
    {
        private readonly IPostTagRepository postTagRepository;
        private readonly ITagService tagService;
        public PostTagService(IPostTagRepository postTagRepository, ITagService tagService) {
            this.postTagRepository = postTagRepository;
            this.tagService = tagService;
        }

        public async Task<BaseResponse<bool>> AddTagsToPost(int postId,List<string> tags) {
            if (!tags.Any()) {
                return new BaseResponse<bool>
                {
                    Description="Теги отсутствуют"
                };
            }
            await tagService.CreateAsync(tags);

            var dbTags = (await tagService.GetAllByNames(tags)).Data.ToList();

            var newTags = new List<PostTag>();

            for (int i = 0; i < dbTags.Count(); i++) {
                newTags.Add(new PostTag { TagId = dbTags[i].Id, PostId= postId });
            }
            try {
                bool res = await postTagRepository.AddTagsToPost(newTags);
            }
            catch (Exception e) {
                return new BaseResponse<bool>
                {
                    Description = "Не удалось добавить теги"
                };
            }
            return new BaseResponse<bool>
            {
                StatusCode = 200
            };
        }

        public async Task<BaseResponse<IEnumerable<Tag>>> GetPostTagsAsync(int postId) {
            IEnumerable<Tag> tags =await postTagRepository.GetPostTagsAsync(postId);
            if (tags != null) {
                return new BaseResponse<IEnumerable<Tag>>
                {
                    Data=tags,
                    StatusCode = 200
                };
            }
            return new BaseResponse<IEnumerable<Tag>>
            {
                Description="Теги отсутствуют"
            };
        }
    }
}
