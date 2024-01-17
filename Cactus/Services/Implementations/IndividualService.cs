using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;
using Cactus.Services.Interfaces;
using System.Security.Claims;

namespace Cactus.Services.Implementations
{
    public class IndividualService:IIndividualService
    {
        private readonly IUserRepository userRepository;
        private readonly IIndividualRepository individualRepository;

        public IndividualService(IUserRepository userRepository, IIndividualRepository individualRepository) {
            this.userRepository = userRepository;
            this.individualRepository = individualRepository;
        }

        public async Task<BaseResponse<Individual>> RegisterIndividual(RegisterIndividualViewModel model,int id) {
            User user = await userRepository.GetAsync(id);
            user.UserRoleId = (int)Models.Enums.UserRole.Individual;
            var newIndividual = new Individual { UrlPage = model.UrlPage, UserId = id };
            await individualRepository.CreateAsync(newIndividual);
            return new BaseResponse<Individual>()
            {
                Data = newIndividual,
                StatusCode = StatusCodes.Status200OK
            };
        }
    }
}
