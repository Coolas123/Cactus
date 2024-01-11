using Cactus.Infrastructure;
using Cactus.Infrastructure.Interfaces;
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
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = "Пользователь с такой почтой зарегистрирован"
                };
            }
            user = await userRepository.GetByUserNameAsync(model.UserName);
            if (user != null)
            {
                return new BaseResponse<ClaimsIdentity>()
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
            var result = Authenticate(user);
            return new BaseResponse<ClaimsIdentity>()
            {
                Data = result,
                Description = "Пользователь создан",
                StatucCode = StatusCodes.Status200OK
            };
        }

        public ClaimsIdentity Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType,user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType,Cactus.Models.Enums.SystemRole.User.ToString()),
                new Claim(ClaimsIdentity.DefaultRoleClaimType,Cactus.Models.Enums.UserRole.Patron.ToString())
            };
            return new ClaimsIdentity(claims, "ApplicationCookie", 
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }

        public async Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model)
        {
            User user = await userRepository.GetByEmailAsync(model.Email);
            if (user == null)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = "Неверный логин"
                };
            }
            if (user.HashPassword != HashPassword.Generate(model.Password))
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = "Неверный пароль"
                };
            }
            var claims = Authenticate(user);
            return new BaseResponse<ClaimsIdentity>()
            {
                Data=claims,
                StatucCode=StatusCodes.Status200OK
            };
        }
    }
}
