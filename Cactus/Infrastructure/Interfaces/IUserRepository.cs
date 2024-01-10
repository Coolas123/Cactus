using Cactus.Models.Database;

namespace Cactus.Infrastructure.Interfaces
{
    public interface IUserRepository:IBaseRepository<User>
    {
        Task<User> GetByEmailAsync(string email);
    }
}
