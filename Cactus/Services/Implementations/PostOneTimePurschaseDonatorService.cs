using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Services.Interfaces;

namespace Cactus.Services.Implementations
{
    public class PostOneTimePurschaseDonatorService : IPostOneTimePurschaseDonatorService
    {
        private readonly IPostOneTimePurschaseDonatorRepository postOneTimePurschaseDonatorRepository;
        public PostOneTimePurschaseDonatorService(IPostOneTimePurschaseDonatorRepository postOneTimePurschaseDonatorRepository) {
            this.postOneTimePurschaseDonatorRepository = postOneTimePurschaseDonatorRepository;
        }

        public async Task<BaseResponse<bool>> AddDonator(int postId, int donatorId) {
            var postDonator = new PostOneTimePurschaseDonator
            {
                PostId = postId,
                DonatorId = donatorId
            };
            try {
                await postOneTimePurschaseDonatorRepository.CreateAsync(postDonator);
            }
            catch {
                return new BaseResponse<bool>
                {
                    Description="Не удалось добавить"
                };
            }
            return new BaseResponse<bool>
            {
                Data=true,
                StatusCode=200
            };
        }

        public async Task<BaseResponse<PostOneTimePurschaseDonator>> GetDonator(int postId, int userId) {
            PostOneTimePurschaseDonator donator = await postOneTimePurschaseDonatorRepository.GetDonator(postId, userId);
            if (donator == null) {
                return new BaseResponse<PostOneTimePurschaseDonator>
                {
                    Description= "Пост недоступен"
                };
            }
            return new BaseResponse<PostOneTimePurschaseDonator>
            {
                Data= donator,
                StatusCode = 200
            };
        }
    }
}
