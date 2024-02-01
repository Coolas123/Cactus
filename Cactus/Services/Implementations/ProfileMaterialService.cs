using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;
using Cactus.Services.Interfaces;

namespace Cactus.Services.Implementations
{
    public class ProfileMaterialService : IProfileMaterialService
    {
        private readonly IProfileMaterialRepository materialRepository;
        private readonly IUserRepository userRepository;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ProfileMaterialService(IProfileMaterialRepository dbContext, IWebHostEnvironment webHostEnvironment, IUserRepository userRepository) {
            this.materialRepository = dbContext;
            this.webHostEnvironment = webHostEnvironment;
            this.userRepository = userRepository;
        }

        public async Task<BaseResponse<string>> ChangeAvatarAsync(IFormFile file,int id) {
            string path = "/Files/Account/Avatars/" + id + file.FileName;
            var avatar = new ProfileMaterial
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

        public async Task<BaseResponse<ProfileMaterial>> GetAvatarAsync(string email) {
            User user = await userRepository.GetByEmailAsync(email);
            ProfileMaterial result = await materialRepository.GetAvatarAsync(user.Id);
            if (result == null) {
                return new BaseResponse<ProfileMaterial>
                {
                    Data=null,
                    Description = "Аватар отсутствует"
                };
            }
            return new BaseResponse<ProfileMaterial>
            {
                Data = result,
                StatusCode=StatusCodes.Status200OK
            };
        }

        public async Task<BaseResponse<ProfileMaterial>> GetAvatarAsync(int id) {
            ProfileMaterial result = await materialRepository.GetAvatarAsync(id);
            if (result == null) {
                return new BaseResponse<ProfileMaterial>
                {
                    Data = null,
                    Description = "Аватарка отсутствует"
                };
            }
            return new BaseResponse<ProfileMaterial>
            {
                Data = result,
                StatusCode = StatusCodes.Status200OK
            };
        }
        public async Task<BaseResponse<ProfileMaterial>> GetBannerAsync(string email) {
            User user = await userRepository.GetByEmailAsync(email);
            ProfileMaterial result = await materialRepository.GetBannerAsync(user.Id);
            if (result == null) {
                return new BaseResponse<ProfileMaterial>
                {
                    Data = null,
                    Description = "Баннер отсутствует"
                };
            }
            return new BaseResponse<ProfileMaterial>
            {
                Data = result,
                StatusCode = StatusCodes.Status200OK
            };
        }

        public async Task<BaseResponse<ProfileMaterial>> GetBannerAsync(int id) {
            ProfileMaterial result = await materialRepository.GetBannerAsync(id);
            if (result == null) {
                return new BaseResponse<ProfileMaterial>
                {
                    Data = null,
                    Description = "Баннер отсутствует"
                };
            }
            return new BaseResponse<ProfileMaterial>
            {
                Data = result,
                StatusCode = StatusCodes.Status200OK
            };
        }

        public async Task<BaseResponse<string>> ChangeBannerAsync(IFormFile file, int id) {
            string path = "/Files/Account/Banners/" + id + file.FileName;
            var banner = new ProfileMaterial
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
            IEnumerable<ProfileMaterial> materiarls =await materialRepository.GetProfileMaterialAsync(id);
            return new BaseResponse<IndividualProfileViewModel>
            {
                Data = new IndividualProfileViewModel
                {
                    AvatarPath = materiarls.Where(x => x.MaterialTypeId == (int)Models.Enums.MaterialType.Avatar).ToArray().Last().Path,
                    BannerPath = materiarls.Where(x => x.MaterialTypeId == (int)Models.Enums.MaterialType.Banner).ToArray().Last().Path
                },
                StatusCode = 200
            };
        }
    }
}

