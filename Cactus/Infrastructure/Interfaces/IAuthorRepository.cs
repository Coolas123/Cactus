using Cactus.Models.Database;

namespace Cactus.Infrastructure.Interfaces
{
    public interface IAuthorRepository:IBaseRepository<Author>
    {
        Task<Author> GetUserByUrlPageAsync(string urlPage);
        Task<Author> GetByUrlPageAsync(string urlPage);
        Task<Author> GetUserByNameAsync(IEnumerable<string> names);
    }
}
