using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Services.Interfaces;

namespace Cactus.Services.Implementations
{
    public class PostDonationOptionService : IPostDonationOptionService
    {
        private readonly IPostDonationOptionRepository postDonationOptionRepository;
        public PostDonationOptionService(IPostDonationOptionRepository postDonationOptionRepository) {
            this.postDonationOptionRepository = postDonationOptionRepository;
        }
        public async Task<BaseResponse<PostDonationOption>> GetOption(int postId) {
            PostDonationOption option = await postDonationOptionRepository.GetOption(postId);
            if (option == null) {
                return new BaseResponse<PostDonationOption>
                {
                    Description="Пост бесплатный"
                };
            }
            return new BaseResponse<PostDonationOption>
            {
                Data=option,
                StatusCode=200
            };
        }
    }
}
