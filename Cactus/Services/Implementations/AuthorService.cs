using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;
using Cactus.Services.Interfaces;
using System.Security.Claims;

namespace Cactus.Services.Implementations
{
    public class AuthorService : IAuthorService
    {
        private readonly IUserService userService;
        private readonly IAuthorRepository authorRepository;
        private readonly IPatronService patronService;

        public AuthorService(IUserService userService, IAuthorRepository authorRepository,
            IPatronService patronService) {
            this.userService = userService;
            this.authorRepository = authorRepository;
            this.patronService = patronService;
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

        public async Task<BaseResponse<ClaimsIdentity>> RegisterAuthor(SettingViewModel model,int id) {
            Author pageExist = await authorRepository.GetByUrlPageAsync(model.RegisterAuthor.UrlPage);
            if (pageExist != null) {
                return new BaseResponse<ClaimsIdentity>
                {
                    Description = "Данный адрес уже используется"
                };
            }
            var newIndividual = new Author { UrlPage = model.RegisterAuthor.UrlPage, UserId = id };
            try {
                await authorRepository.CreateAsync(newIndividual);
                var resultPatron = await patronService.DaeleteUser(id);
                if (resultPatron.StatusCode != 200) {
                    return new BaseResponse<ClaimsIdentity>
                    {
                        Description = resultPatron.Description
                    };
                }
                BaseResponse<ClaimsIdentity> result = await userService.ChangeRoleToAuthor(id);
                if (result.StatusCode != 200) {
                    return new BaseResponse<ClaimsIdentity>
                    {
                        Description = result.Description
                    };
                }
                result.Data.AddClaim(new Claim("UrlPage", model.RegisterAuthor.UrlPage));
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
