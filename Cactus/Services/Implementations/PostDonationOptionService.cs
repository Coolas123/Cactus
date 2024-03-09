using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace Cactus.Services.Implementations
{
    public class PostDonationOptionService : IPostDonationOptionService
    {
        private readonly IPostDonationOptionRepository postDonationOptionRepository;
        public PostDonationOptionService(IPostDonationOptionRepository postDonationOptionRepository) {
            this.postDonationOptionRepository = postDonationOptionRepository;
        }

        public async Task<BaseResponse<bool>> AddOptionToPostAsync(int postId, int optionId) {
            var PostDonationOption = new PostDonationOption
            {
                PostId=postId,
                DonationOptionId= optionId
            };
            try {
                await postDonationOptionRepository.AddOptionToPostAsync(PostDonationOption);
            }
            catch (Exception ex) {
                return new BaseResponse<bool>
                {
                    Description = "Не удалось добавить опцию"
                };
            }
            return new BaseResponse<bool>
            {
                Data = true,
                StatusCode=200
            };
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
