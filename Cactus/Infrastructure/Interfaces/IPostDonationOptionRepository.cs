using Cactus.Models.Database;

namespace Cactus.Infrastructure.Interfaces
{
    public interface IPostDonationOptionRepository:IBaseRepository<PostDonationOption>
    {
        Task<PostDonationOption> GetOption(int postId);
        Task<bool> AddOptionToPostAsync(PostDonationOption entity);
    }
}
