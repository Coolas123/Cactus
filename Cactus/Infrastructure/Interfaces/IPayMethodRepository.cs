using Cactus.Models.Database;

namespace Cactus.Infrastructure.Interfaces
{
    public interface IPayMethodRepository:IBaseRepository<PayMethod>
    {
        Task<IEnumerable<PayMethod>> GetMethodsAsync();
    }
}
