using Cactus.Models.Database;

namespace Cactus.Infrastructure.Interfaces
{
    public interface ITagRepository:IBaseRepository<Tag>
    {
        Task<IEnumerable<Tag>> GetAllByNamesAsync(IEnumerable<string> tags);
        Task<bool> CreateAsync(IEnumerable<Tag> tags);
    }
}
