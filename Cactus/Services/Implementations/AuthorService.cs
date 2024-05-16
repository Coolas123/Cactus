using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;
using Cactus.Services.Interfaces;
using System.Drawing;
using System.Security.Claims;

namespace Cactus.Services.Implementations
{
    public class AuthorService : IAuthorService
    {
        private readonly IUserService userService;
        private readonly IAuthorRepository authorRepository;
        private readonly IProfileMaterialService profileMaterialService;

        public AuthorService(IUserService userService, IAuthorRepository authorRepository,
            IProfileMaterialService profileMaterialService) {
            this.userService = userService;
            this.authorRepository = authorRepository;
            this.profileMaterialService = profileMaterialService;
        }

        public async Task<ModelErrorsResponse<Author>> ChangeSettingAsync(NewAuthorSettingViewModel model, int authorId) {
            var response = new ModelErrorsResponse<Author>();
            if (model.BannerFile != null) {
                var image = Image.FromStream(model.BannerFile.OpenReadStream());
                if (image.Width > 1900 || image.Height > 250) {
                    response.Descriptions.Add("NewSettingViewModel.BannerFile", "Баннер должен иметь разрешение не более чем 1900px на 250px");
                }
                else {
                    await profileMaterialService.ChangeBannerAsync(model.BannerFile, authorId);
                    response.StatusCode = 200;
                    response.IsSettingChanged = true;
                }
            }

            BaseResponse<Author> author = await GetAsync(authorId);
            if (model.Description != author.Data.Description) {
                author.Data.Description = model.Description;
                await authorRepository.Update(author.Data);
                response.StatusCode = 200;
            }
            return response;
        }

        public async Task<BaseResponse<Author>> DaeleteAuthor(int id) {
            try {
                Author individual = await authorRepository.GetAsync(id);
                await authorRepository.DeleteAsync(individual);
                return new BaseResponse<Author>
                {
                    Description = "Удалена роль Individual",
                    StatusCode = 200
                };
            }
            catch {
                return new BaseResponse<Author>
                {
                    Description = "Не удалось удалить роль Individual"
                };
            }
        }

        public async Task<BaseResponse<Author>> GetAsync(int id) {
            Author individual =  await authorRepository.GetAsync(id);
            if (individual == null) {
                return new BaseResponse<Author>
                {
                    Description="Пользователль не найден"
                };
            }
            return new BaseResponse<Author>
            {
                Data=individual,
                StatusCode=200
            };
        }

        public async Task<BaseResponse<Author>> GetBuyUrlPage(string urlPage) {
            Author inidividual =await authorRepository.GetByUrlPageAsync(urlPage);
            return new BaseResponse<Author>
            {
                Data = inidividual,
                StatusCode = 200
            };
        }

        public async Task<BaseResponse<User>> GetUserAsync(int id) {
            Author individual = await authorRepository.GetAsync(id);
            if (individual == null) {
                return new BaseResponse<User>
                {
                    Description = "профиль не найден"
                };
            }
            return new BaseResponse<User>
            {
                Data = individual.User,
                StatusCode = 200
            };
        }

        public async Task<BaseResponse<Author>> GetUserByNameAsync(IEnumerable<string> names) {
            Author author = await authorRepository.GetUserByNameAsync(names);
            if (author != null) {
                return new BaseResponse<Author>
                {
                    Data=author,
                    StatusCode=200
                };
            }
            return new BaseResponse<Author>
            {
                Description="Автор не найден"
            };
        }

        public async Task<BaseResponse<User>> GetUserByUrlPageAsync(string urlPage) {
            Author individual = await authorRepository.GetUserByUrlPageAsync(urlPage);
            if (individual == null) {
                return new BaseResponse<User>
                {
                    Description = "профиль не найден"
                };
            }
            return new BaseResponse<User>
            {
                Data = individual.User,
                StatusCode = 200
            };
        }

        public async Task<BaseResponse<ClaimsIdentity>> RegisterAuthor(RegisterAuthorViewModel model,int id) {
            Author pageExist = await authorRepository.GetByUrlPageAsync(model.UrlPage);
            if (pageExist != null) {
                return new BaseResponse<ClaimsIdentity>
                {
                    Description = "Данный адрес уже используется"
                };
            }
            var newIndividual = new Author { UrlPage = model.UrlPage, UserId = id };
            try {
                await authorRepository.CreateAsync(newIndividual);

                BaseResponse<ClaimsIdentity> result = await userService.ChangeRoleToAuthor(id);
                if (result.StatusCode != 200) {
                    return new BaseResponse<ClaimsIdentity>
                    {
                        Description = result.Description
                    };
                }
                result.Data.AddClaim(new Claim("UrlPage", model.UrlPage));
                return new BaseResponse<ClaimsIdentity>
                {
                    Data = result.Data,
                    Description = result.Description,
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch {
                return new BaseResponse<ClaimsIdentity>
                {
                    Description = "Не удалось добавить роль Author к пользователю"
                };
            }
        }
    }
}
