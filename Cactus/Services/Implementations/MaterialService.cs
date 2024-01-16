using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Services.Interfaces;

namespace Cactus.Services.Implementations
{
    public class MaterialService : IMaterialService
    {
        private readonly IMaterialRepository materialRepository;
        private readonly IUserRepository userRepository;
        private readonly IWebHostEnvironment webHostEnvironment;

        public MaterialService(IMaterialRepository dbContext, IWebHostEnvironment webHostEnvironment, IUserRepository userRepository) {
            this.materialRepository = dbContext;
            this.webHostEnvironment = webHostEnvironment;
            this.userRepository = userRepository;
        }

        public async Task<BaseResponse<bool>> ChangeAvatarAsync(IFormFile file,string email) {
            User user = await userRepository.GetByEmailAsync(email);
            string path = "/Files/Account/Avatars/" + user.Id + file.FileName;
            var avatar = new Material
            {
                UserId = user.Id,
                MaterialTypeId = (int)Models.Enums.MaterialType.Avatar,
                Title = file.FileName,
                Path = path
            };
            try {
                using (var fStream = new FileStream(webHostEnvironment.WebRootPath + path, FileMode.Create)) {
                    await file.CopyToAsync(fStream);
                }
                await materialRepository.UpdateAvatarAsync(avatar);
            }
            catch {
                return new BaseResponse<bool>
                {
                    Description = "Не удалось обновить аватарку"
                };
            }
            return new BaseResponse<bool>
            {
                Description = "Аватарка обновлена",
                StatusCode = StatusCodes.Status200OK
            };
        }

        public async Task<BaseResponse<Material>> GetAvatarAsync(string email) {
            User user = await userRepository.GetByEmailAsync(email);
            Material result = await materialRepository.GetAvatarAsync(user.Id);
            if (result == null) {
                return new BaseResponse<Material>
                {
                    Data=null,
                    Description = "Аватар отсутствует"
                };
            }
            return new BaseResponse<Material>
            {
                Data = result,
                StatusCode=StatusCodes.Status200OK
            };
        }

        public async Task<BaseResponse<Material>> GetAvatarAsync(int id) {
            User user = await userRepository.GetAsync(id);
            Material result = await materialRepository.GetAvatarAsync(user.Id);
            if (result == null) {
                return new BaseResponse<Material>
                {
                    Data = null,
                    Description = "Аватарка отсутствует"
                };
            }
            return new BaseResponse<Material>
            {
                Data = result,
                StatusCode = StatusCodes.Status200OK
            };
        }
    }
}

