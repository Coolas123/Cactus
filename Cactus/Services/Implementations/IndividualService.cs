using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;
using Cactus.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace Cactus.Services.Implementations
{
    public class IndividualService:IIndividualService
    {
        private readonly IUserService userService;
        private readonly IIndividualRepository individualRepository;
        private readonly IPatronService patronService;

        public IndividualService(IUserService userService, IIndividualRepository individualRepository,
            IPatronService patronService) {
            this.userService = userService;
            this.individualRepository = individualRepository;
            this.patronService = patronService;
        }

        public async Task<BaseResponse<Individual>> GetBuyUrlPage(string urlPage) {
            Individual inidividual =await individualRepository.GetByUrlPageAsync(urlPage);
            return new BaseResponse<Individual>
            {
                Data = inidividual,
                StatusCode = 200
            };
        }

        public async Task<BaseResponse<ClaimsIdentity>> RegisterIndividual(SettingViewModel model,int id) {
            Individual pageExist = await individualRepository.GetByUrlPageAsync(model.IndividualSettings.UrlPage);
            if (pageExist != null) {
                return new BaseResponse<ClaimsIdentity>
                {
                    Description = "Данный адрес уже используется"
                };
            }
            var newIndividual = new Individual { UrlPage = model.IndividualSettings.UrlPage, UserId = id };
            try {
                await individualRepository.CreateAsync(newIndividual);
                var resultPatron = await patronService.DaeleteUser(id);
                if (resultPatron.StatusCode != 200) {
                    return new BaseResponse<ClaimsIdentity>
                    {
                        Description = resultPatron.Description
                    };
                }
                BaseResponse<ClaimsIdentity> result = await userService.ChangeRoleToIndividual(id);
                if (result.StatusCode != 200) {
                    return new BaseResponse<ClaimsIdentity>
                    {
                        Description = result.Description
                    };
                }
                result.Data.AddClaim(new Claim("UrlPage", model.IndividualSettings.UrlPage));
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
                    Description = "Не удалось добавить роль Individual к пользователю"
                };
            }
        }
    }
}
