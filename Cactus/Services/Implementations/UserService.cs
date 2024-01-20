using Cactus.Infrastructure;
using Cactus.Infrastructure.Interfaces;
using Cactus.Infrastructure.Repositories;
using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;
using Cactus.Services.Interfaces;
using System.Security.Claims;

namespace Cactus.Services.Implementations
{
    public class UserService : IUserService
    {
		private readonly IUserRepository userRepository;

		public UserService (IUserRepository dbContext)
		{
			this.userRepository = dbContext;
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

        public async Task<ModelErrorsResponse<ClaimsIdentity>> ChangeSettingsAsync(ProfileViewModel model, int id) {
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
    }
}
