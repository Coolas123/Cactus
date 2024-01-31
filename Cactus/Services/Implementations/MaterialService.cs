using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;
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

        public async Task<BaseResponse<string>> ChangeAvatarAsync(IFormFile file,int id) {
            string path = "/Files/Account/Avatars/" + id + file.FileName;
            var avatar = new Material
            {
                UserId = id,
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
                return new BaseResponse<string>
                {
                    Description = "Не удалось обновить аватарку"
                };
            }
            return new BaseResponse<string>
            {
                Data=path,
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
            Material result = await materialRepository.GetAvatarAsync(id);
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
        public async Task<BaseResponse<Material>> GetBannerAsync(string email) {
            User user = await userRepository.GetByEmailAsync(email);
            Material result = await materialRepository.GetBannerAsync(user.Id);
            if (result == null) {
                return new BaseResponse<Material>
                {
                    Data = null,
                    Description = "Баннер отсутствует"
                };
            }
            return new BaseResponse<Material>
            {
                Data = result,
                StatusCode = StatusCodes.Status200OK
            };
        }

        public async Task<BaseResponse<Material>> GetBannerAsync(int id) {
            Material result = await materialRepository.GetBannerAsync(id);
            if (result == null) {
                return new BaseResponse<Material>
                {
                    Data = null,
                    Description = "Баннер отсутствует"
                };
            }
            return new BaseResponse<Material>
            {
                Data = result,
                StatusCode = StatusCodes.Status200OK
            };
        }

        public async Task<BaseResponse<string>> ChangeBannerAsync(IFormFile file, int id) {
            string path = "/Files/Account/Banners/" + id + file.FileName;
            var banner = new Material
            {
                UserId = id,
                MaterialTypeId = (int)Models.Enums.MaterialType.Banner,
                Title = file.FileName,
                Path = path
            };
            try {
                using (var fStream = new FileStream(webHostEnvironment.WebRootPath + path, FileMode.Create)) {
                    await file.CopyToAsync(fStream);
                }
                await materialRepository.UpdateBannerAsync(banner);
            }
            catch {
                return new BaseResponse<string>
                {
                    Description = "Не удалось обновить баннер"
                };
            }
            return new BaseResponse<string>
            {
                Data=path,
                Description = "Баннер обновлен",
                StatusCode = StatusCodes.Status200OK
            };
        }

        public async Task<BaseResponse<IndividualProfileViewModel>> GetProfileMaterials(int id) {
            Material avatarMaterial =await materialRepository.GetAvatarAsync(id);
            Material bannerMaterial =await materialRepository.GetBannerAsync(id);
            return new BaseResponse<IndividualProfileViewModel>
            {
                Data = new IndividualProfileViewModel
                {
                    AvatarPath = avatarMaterial.Path,
                    BannerPath = bannerMaterial.Path
                },
                StatusCode = 200
            };
        }
    }
}

