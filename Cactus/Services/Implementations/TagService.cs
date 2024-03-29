using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Services.Interfaces;

namespace Cactus.Services.Implementations
{
    public class TagService : ITagService
    {
        private readonly ITagRepository tagRepository;
        public TagService(ITagRepository tagRepository) {
            this.tagRepository = tagRepository;
        }

        public async Task<BaseResponse<bool>> CreateAsync(IEnumerable<string> tags) {
            var newTags = new List<Tag>();
            foreach (var tag in tags) {
                newTags.Add(new Tag { Name = tag });
            }
            try {
                await tagRepository.CreateAsync(newTags);
            }
            catch (Exception e) {
                return new BaseResponse<bool>
                {
                    Description = "Не удалось сохранить теги"
                };
            }
            return new BaseResponse<bool>
            {
                Description = "Теги сохранены",
                StatusCode = 200
            };
        }

        public async Task<BaseResponse<IEnumerable<Tag>>> GetAll() {
            IEnumerable<Tag> tags =await tagRepository.SelectAsync();
            if (tags.Any()) {
                return new BaseResponse<IEnumerable<Tag>>
                {
                    Description = "теги отсутствуют"
                };
            }
            return new BaseResponse<IEnumerable<Tag>>
            {
                Data = tags,
                StatusCode = 200
            };
        }

        public async Task<BaseResponse<IEnumerable<Tag>>> GetAllByNames(IEnumerable<string> tags) {
            IEnumerable<Tag> Dbtags= await tagRepository.GetAllByNamesAsync(tags);
            if (!Dbtags.Any())
            {
                return new BaseResponse<IEnumerable<Tag>>
                {
                    Description="Теги не найдены"
                };
            }
            return new BaseResponse<IEnumerable<Tag>>
            {
                Data = Dbtags,
                StatusCode = 200
            };
        }
    }
}
