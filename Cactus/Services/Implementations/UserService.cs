using Cactus.Infrastructure;
using Cactus.Infrastructure.Interfaces;
using Cactus.Infrastructure.Repositories;
using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;
using Cactus.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Claims;

namespace Cactus.Services.Implementations
{
    public class UserService : IUserService
    {
		private readonly IUserRepository userRepository;
		private readonly IIndividualRepository individualRepository;
		private readonly IMaterialService materialService;
		private readonly IMemoryCache cache;

		public UserService (IUserRepository userRepository, IMemoryCache cache,
            IIndividualRepository individualRepository, IMaterialService materialService)
		{
			this.userRepository = userRepository;
            this.cache = cache;
            this.individualRepository = individualRepository;
            this.materialService = materialService;
		}

        public async Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model)
        {
            User user = await userRepository.GetByEmailAsync(model.Email);
            if (user != null)
            {
                return new BaseResponse<ClaimsIdentity>
                {
                    Description = "Пользователь с такой почтой зарегистрирован"
                };
            }
            user = await userRepository.GetByUserNameAsync(model.UserName);
            if (user != null)
            {
                return new BaseResponse<ClaimsIdentity>
                {
                    Description = "Аккаунт с таким прозвищем уже существует"
                };
            }
            user = new User
            {
                UserName=model.UserName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateOfBirth = model.DateOfBirth.ToUniversalTime(),
                Gender = model.Gender,
                Email = model.Email,
                CountryId = (int)model.Country,
                SystemRoleId = (int)Models.Enums.SystemRole.User,
                UserRoleId = (int)Models.Enums.UserRole.Patron,
                HashPassword = HashPassword.Generate(model.Password)
            };
            await userRepository.CreateAsync(user);
            User dbUser =await userRepository.GetByEmailAsync(model.Email);
            var result = Authenticate(user, dbUser.Id);
            return new BaseResponse<ClaimsIdentity>
            {
                Data = result,
                Description = "Пользователь создан",
                StatusCode = StatusCodes.Status200OK
            };
        }

        public ClaimsIdentity Authenticate(User user, int id)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType,user.Email),
                new Claim("Id",id.ToString()),
                new Claim(ClaimsIdentity.DefaultRoleClaimType,
                ((Models.Enums.SystemRole)Enum
                .GetValues(typeof(Models.Enums.SystemRole))
                .GetValue(user.SystemRoleId-1))
                .ToString()),
                new Claim(ClaimsIdentity.DefaultRoleClaimType,
                ((Models.Enums.UserRole)Enum
                .GetValues(typeof(Models.Enums.UserRole))
                .GetValue(user.UserRoleId-1))
                .ToString())
            };
            return new ClaimsIdentity(claims, "ApplicationCookie", 
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }

        public async Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model)
        {
            User user = await userRepository.GetByEmailAsync(model.Email);
            if (user == null)
            {
                return new BaseResponse<ClaimsIdentity>
                {
                    Description = "Неверный логин"
                };
            }
            if (user.HashPassword != HashPassword.Generate(model.Password))
            {
                return new BaseResponse<ClaimsIdentity>
                {
                    Description = "Неверный пароль"
                };
            }
            var claims = Authenticate(user, user.Id);
            return new BaseResponse<ClaimsIdentity>()
            {
                Data=claims,
                StatusCode = StatusCodes.Status200OK
            };
        }

        public async Task<ModelErrorsResponse<ClaimsIdentity>> ChangeSettingsAsync(SettingViewModel model, int id) {
            User user = await userRepository.GetAsync(id);

            ModelErrorsResponse<ClaimsIdentity> response = new ModelErrorsResponse<ClaimsIdentity>()
            {
                Descriptions = new Dictionary<string, string>()
            };

            if (model.UserName != null&& model.UserName!=user.UserName) {
                User result = await userRepository.GetByUserNameAsync(model.UserName);
                if (result != null) {
                    response.Descriptions.Add(nameof(model.UserName), "Пользователь с таким прозвищем уже существует");
                }
                else user.UserName = model.UserName;
            }
            if (model.DateOfBirth != DateTime.MinValue &&
                DateOnly.FromDateTime(model.DateOfBirth) == DateOnly.FromDateTime(user.DateOfBirth))
                user.DateOfBirth = model.DateOfBirth.ToUniversalTime();

            if (model.Password != null)
                user.HashPassword = HashPassword.Generate(model.Password);

            if (model.Email != null && model.Email != user.Email) {
                User result = await userRepository.GetByEmailAsync(model.Email);
                if (result != null) {
                    response.Descriptions.Add(nameof(model.UserName), "Пользователь с такой почтой уже существует");
                }
                else {
                    user.Email = model.Email;
                    response.Data = Authenticate(user, id);
                }
            }
            await userRepository.Update(user);
            if (response.Descriptions.Count() == 0)
                response.StatusCode = StatusCodes.Status200OK;
            return response;
        }

        public async Task<BaseResponse<ClaimsIdentity>> ChangeRoleToIndividual(int id) {
            User user = await userRepository.GetAsync(id);
            if (user == null) {
                return new BaseResponse<ClaimsIdentity>
                {
                    Description="Пользователь не найден"
                };
            }
            user.UserRoleId = (int)Models.Enums.UserRole.Individual;
            await userRepository.Update(user);
            return new BaseResponse<ClaimsIdentity>
            {
                Description = "Роль обновлена на Individual",
                Data = Authenticate(user, id),
                StatusCode = 200
            };
        }

        public async Task<BaseResponse<User>> AddToCacheAsync(string email) {
            IndividualProfileViewModel profile;
            cache.TryGetValue("IndividualProfile", out profile);
            if (profile == null)
                profile = new IndividualProfileViewModel();
            profile.User = await userRepository.GetByEmailAsync(email);
            cache.Set("IndividualProfile", profile);
            return new BaseResponse<User> { Data = profile.User };
        }

        public async Task<BaseResponse<IndividualProfileViewModel>> GetPrifileByUrlPage(string urlPage) {
            Individual individual = await individualRepository.GetUserByUrlPageAsync(urlPage);
            if (individual!=null) {
                return new BaseResponse<IndividualProfileViewModel>
                {
                    Description = "Профиль не найден"
                };
            }
            BaseResponse<IndividualProfileViewModel> profileMaterials = await materialService.GetProfileMaterials(individual.UserId);
            profileMaterials.Data.User = individual.User;
            return profileMaterials;
        }

        public async Task<BaseResponse<User>> GetUserByUrlPageAsync(string urlPage) {
            Individual individual = await individualRepository.GetUserByUrlPageAsync(urlPage);
            if (individual==null) {
                return new BaseResponse<User>
                {
                    Description = "Профиль не найден"
                };
            }
            return new BaseResponse<User>
            {
                Data= individual.User,
                StatusCode=200
            };
        }
    }
}
