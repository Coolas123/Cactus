using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;
using Cactus.Services.Interfaces;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.Caching.Memory;

namespace Cactus.Components
{
    public class IndividualProfile:ViewComponent
    {
        private readonly IMemoryCache cache;
        private readonly IIndividualRepository individualRepository;
        private readonly IMaterialService materialService;
        public IndividualProfile(IMemoryCache cache, IIndividualRepository individualRepository,
            IMaterialService materialService) {
            this.cache = cache;
            this.individualRepository = individualRepository;
            this.materialService = materialService;
        }

        public async Task<string> InvokeAsync(string property,int id) {
            IndividualProfileViewModel profile;
            cache.TryGetValue("IndividualProfile", out profile);
            if (profile == null) {
                profile= new IndividualProfileViewModel();
                Individual individual = await individualRepository.GetAsync(id);
                profile.UrlPage = individual.UrlPage;

                BaseResponse<Material> result = await materialService.GetAvatarAsync(id);
                if (result.StatusCode == StatusCodes.Status200OK) {
                    profile.AvatarPath = result.Data.Path;
                }
                cache.Set("IndividualProfile", profile);
            }
            var response = typeof(IndividualProfileViewModel).GetProperty(property).GetValue(profile);
            var t = response?.ToString() ?? "#";
            return t;
        }
    }
}
