using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;

namespace Cactus.Services.Interfaces
{
    public interface IComplainService
    {
        Task<BaseResponse<IEnumerable<ComplainViewModel>>> GetNotReviewedComplains(DateTime date);
        Task<BaseResponse<bool>> AddComplain(NewComplainViewModel model);
    }
}
