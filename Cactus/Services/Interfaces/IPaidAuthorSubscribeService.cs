using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;

namespace Cactus.Services.Interfaces
{
    public interface IPaidAuthorSubscribeService
    {
        Task<BaseResponse<bool>> SubscribeToAuthor(NewPaidSubscribeViewModel model);
        Task<BaseResponse<IEnumerable<PaidAuthorSubscribe>>> GetCurrentSubscribes(int donatorId);
    }
}
