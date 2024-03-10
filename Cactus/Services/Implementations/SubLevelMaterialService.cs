using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Services.Interfaces;

namespace Cactus.Services.Implementations
{
    public class SubLevelMaterialService : ISubLevelMaterialService
    {
        private readonly ISubLevelMaterialRepository subLevelMaterialRpository;
        private readonly IWebHostEnvironment webHostEnvironment;
        public SubLevelMaterialService(ISubLevelMaterialRepository subLevelMaterialRpository, IWebHostEnvironment webHostEnvironment) {
            this.subLevelMaterialRpository = subLevelMaterialRpository;
            this.webHostEnvironment = webHostEnvironment;
        }

        public async Task<BaseResponse<SubLevelMaterial>> GetCover(int id) {
            SubLevelMaterial material = await subLevelMaterialRpository.GetAsync(id);
            if (material == null) {
                return new BaseResponse<SubLevelMaterial>
                {
                    Description="Обложка отсутствует"
                };
            }
            return new BaseResponse<SubLevelMaterial>
            {
                Data= material,
                StatusCode= 200
            };
        }

        public async Task<BaseResponse<IEnumerable<SubLevelMaterial>>> GetMaterialsAsync(int authorId) {
            IEnumerable<SubLevelMaterial> materials = await subLevelMaterialRpository.GetMaterialsAsync(authorId);
            if (materials == null) {
                return new BaseResponse<IEnumerable<SubLevelMaterial>>
                {
                    Description = "Подписки не найдены"
                };
            }
            return new BaseResponse<IEnumerable<SubLevelMaterial>>
            {
                Data= materials,
                StatusCode= 200
            };
        }

        public async Task<BaseResponse<string>> UpdateCoverAsync(IFormFile file, int id) {
            string path = "/Files/SubLevel/" + id + file.FileName;
            var cover = new SubLevelMaterial
            {
                DonationOptionId = id,
                MaterialTypeId = (int)Models.Enums.MaterialType.SubLevelCover,
                Title = file.FileName,
                Path = path
            };
            try {
                using (var fStream = new FileStream(webHostEnvironment.WebRootPath + path, FileMode.Create)) {
                    await file.CopyToAsync(fStream);
                }
                await subLevelMaterialRpository.UpdateCoverAsync(cover);
            }
            catch {
                return new BaseResponse<string>
                {
                    Description = "Не удалось обновить обложку"
                };
            }
            return new BaseResponse<string>
            {
                Data = path,
                Description = "Обложка обновлена",
                StatusCode = StatusCodes.Status200OK
            };
        }
    }
}
