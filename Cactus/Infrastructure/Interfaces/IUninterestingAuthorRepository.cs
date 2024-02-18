using Cactus.Models.Database;

namespace Cactus.Infrastructure.Interfaces
{
    public interface IUninterestingAuthorRepository:IBaseRepository<UninterestingAuthor>
    {
        Task<UninterestingAuthor> GetAuthorAsync(int userId, int authorId);
        Task<IEnumerable<UninterestingAuthor>> GetPagingAuthorsAsync(int userId, int authorPage, int pageSize);
        Task<IEnumerable<UninterestingAuthor>> GetAuthorsAsync(int userId);
    }
}
