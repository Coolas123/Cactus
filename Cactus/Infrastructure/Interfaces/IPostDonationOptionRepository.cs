using Cactus.Models.Database;

namespace Cactus.Infrastructure.Interfaces
{
    public interface IPostDonationOptionRepository : IBaseRepository<PostDonationOption>
    {
        Task<PostDonationOption> GetOptionAsync(int postId);
        Task<bool> AddOptionToPostAsync(PostDonationOption entity);
    }
}