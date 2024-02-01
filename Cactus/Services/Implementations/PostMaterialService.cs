using Cactus.Infrastructure.Interfaces;
using Cactus.Infrastructure.Repositories;
using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;
using Cactus.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;

namespace Cactus.Services.Implementations
{
    public class PostMaterialService : IPostMaterialService
    {
        private readonly IPostMaterialRepository postMaterialRepository;
        private readonly IWebHostEnvironment webHostEnvironment;
        public PostMaterialService(IPostMaterialRepository postMaterialRepository, IWebHostEnvironment webHostEnvironment) {
            this.postMaterialRepository = postMaterialRepository;
            this.webHostEnvironment = webHostEnvironment;
        }

        public async Task<BaseResponse<PostMaterial>> AddPhotoAsync(IFormFile file, int id) {
            string path = "/Files/Posts/Photos/" + id + file.FileName;
            var photo = new PostMaterial
            {
                PostId=id,
                MaterialTypeId = (int)Models.Enums.MaterialType.PostPhoto,
                Title = file.FileName,
                Path = path
            };
            try {
                using (var fStream = new FileStream(webHostEnvironment.WebRootPath + path, FileMode.Create)) {
                    await file.CopyToAsync(fStream);
                }
                await postMaterialRepository.CreateAsync(photo);
            }
            catch {
                return new BaseResponse<PostMaterial>
                {
                    Description = "Не удалось загрузить фото"
                };
            }
            return new BaseResponse<PostMaterial>
            {
                Data = photo,
                StatusCode = 200
            };
        }

        public async Task<BaseResponse<PostMaterial>> GetPhotoAsync(int id) {
            PostMaterial photo = await postMaterialRepository.GetPhotoAsync(id);
            if (photo == null) {
                return new BaseResponse<PostMaterial>
                {
                    Description = "Баннер отсутствует"
                };
            }
            return new BaseResponse<PostMaterial>
            {
                Data = photo,
                StatusCode = StatusCodes.Status200OK
            };
        }
    }
}
