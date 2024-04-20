using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;
using Cactus.Services.Interfaces;
using SportsStore.Models;

namespace Cactus.Services.Implementations
{
    public class UninterestingAuthorService : IUninterestingAuthorService {
        private readonly IUninterestingAuthorRepository uninterestingAuthorRepository;
        public UninterestingAuthorService(IUninterestingAuthorRepository uninterestingAuthorRepository) {
            this.uninterestingAuthorRepository = uninterestingAuthorRepository;
        }

        public async Task<BaseResponse<UninterestingAuthor>> AddUninterestingAuthor(int userId, int authorId) {
            UninterestingAuthor author = await uninterestingAuthorRepository.GetAuthorAsync(userId, authorId);
            if (author != null) {
                return new BaseResponse<UninterestingAuthor>
                {
                    Description = "Автор уже добавлен в список неинтересного"
                };
            }
            author = new UninterestingAuthor()
            {
                UserId = userId,
                AuthorId = authorId,
            };
            await uninterestingAuthorRepository.CreateAsync(author);
            return new BaseResponse<UninterestingAuthor>
            {
                Data = author,
                Description = "Автор добавлен в список неинтересного",
                StatusCode = 200
            };
        }

        public async Task<BaseResponse<UninterestingAuthor>> GetAuthorAsync(int userId, int authorId) {
            UninterestingAuthor uninterestingAuthor = await uninterestingAuthorRepository.GetAuthorAsync(userId, authorId);
            if (uninterestingAuthor == null) {
                return new BaseResponse<UninterestingAuthor>
                {
                    Description = "Автор не в списке неинтересного"
                };
            }
            return new BaseResponse<UninterestingAuthor>
            {
                Data = uninterestingAuthor,
                StatusCode = 200
            };
        }

        public async Task<BaseResponse<IEnumerable<UninterestingAuthor>>> GetPagingUninterestingsAsync(int userId, int authorPage, int pageSize) {
            IEnumerable<UninterestingAuthor> authors = await uninterestingAuthorRepository.GetPagingAuthorsAsync(userId, authorPage, pageSize);
            if (authors.Count() == 0) {
                return new BaseResponse<IEnumerable<UninterestingAuthor>>
                {
                    Description = "Список пуст"
                };
            }
            return new BaseResponse<IEnumerable<UninterestingAuthor>>
            {
                Data = authors,
                StatusCode = 200
            };
        }

        public async Task<BaseResponse<IEnumerable<UninterestingAuthor>>> GetUninterestingAuthorsViewAsync(int userId) {
            BaseResponse<IEnumerable<UninterestingAuthor>> allAuthors = await GetUninterestingsAsync(userId);
            if (allAuthors.StatusCode == 200) {
                return new BaseResponse<IEnumerable<UninterestingAuthor>>
                {
                    Data = allAuthors.Data,
                    StatusCode = 200
                };
            }
            return new BaseResponse<IEnumerable<UninterestingAuthor>>
            {
                Description="Авторы не найдены"
            };
        }


        public async Task<BaseResponse<IEnumerable<UninterestingAuthor>>> GetUninterestingsAsync(int userId) {
            IEnumerable<UninterestingAuthor> authors = await uninterestingAuthorRepository.GetAuthorsAsync(userId);
            if (authors.Count() == 0) {
                return new BaseResponse<IEnumerable<UninterestingAuthor>>
                {
                    Description = "Список пуст",
                };
            }
            return new BaseResponse<IEnumerable<UninterestingAuthor>>
            {
                Data = authors,
                StatusCode = 200
            };
        }

        public async Task<BaseResponse<bool>> RemoveUninterestingAuthor(int userId, int authorId) {
            UninterestingAuthor uninteresting =await uninterestingAuthorRepository.GetAuthorAsync(userId, authorId);
            bool response = await uninterestingAuthorRepository.DeleteAsync(uninteresting);
            if (!response) {
                return new BaseResponse<bool>
                {
                    Description="Не удалось удалить из списка"
                };
            }
            return new BaseResponse<bool>
            {
                Data=true,
                StatusCode=200
            };
        }
    }
}
