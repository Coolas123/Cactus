namespace Cactus.Infrastructure.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<T> GetAsync(int id);
        Task<bool> DeleteAsync(T entity);
        Task<bool> CreateAsync(T entity);
        Task<IEnumerable<T>> SelectAsync();
    }
}
