using Cactus.Infrastructure.Interfaces;
using Cactus.Infrastructure.Repositories;
using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;
using Cactus.Services.Interfaces;
using System.Security.Claims;

namespace Cactus.Services.Implementations
{
    public class LegalService:ILegalService
    {
        private readonly IUserService userService;
        private readonly ILegalRepository legalRepository;
        private readonly IIndividualService individualService;

        public LegalService(IUserService userService, ILegalRepository legalRepository,
            IIndividualService individualService) {
            this.userService = userService;
            this.legalRepository = legalRepository;
            this.individualService = individualService;
        }

        public async Task<BaseResponse<Legal>> GetAsync(int id) {
            return new BaseResponse<Legal>{
                Data = await legalRepository.GetAsync(id)
            };
        }

        public async Task<BaseResponse<ClaimsIdentity>> RegisterLegal(RegisterLegalViewModel model, int id) {
            Legal pageExist = await legalRepository.GetByUrlPageAsync(model.UrlPage);
            if (pageExist != null) {
                return new BaseResponse<ClaimsIdentity>
                {
                    Description = "Данный адрес уже используется"
                };
            }
            var newLegal = new Legal { UrlPage = model.UrlPage, UserId = id };
            try {
                await legalRepository.CreateAsync(newLegal);
                var resultIndividual = await individualService.DaeleteIndividual(id);
                if (resultIndividual.StatusCode != 200) {
                    return new BaseResponse<ClaimsIdentity>
                    {
                        Description = resultIndividual.Description
                    };
                }
                BaseResponse<ClaimsIdentity> result = await userService.ChangeRoleToLegal(id);
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
                    Description = "Не удалось добавить роль Legal к пользователю"
                };
            }
        }
    }
}
