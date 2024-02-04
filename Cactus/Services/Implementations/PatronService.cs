using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Services.Interfaces;

namespace Cactus.Services.Implementations
{
    public class PatronService : IPatronService
    {
        private readonly IPatronRepository patronRepository;
        public PatronService(IPatronRepository patronRepository) {
            this.patronRepository = patronRepository;
        }
        public async Task<BaseResponse<Patron>> DaeleteUser(int id) {
            try {
                Patron patron = await patronRepository.GetAsync(id);
                await patronRepository.DeleteAsync(patron);
                return new BaseResponse<Patron>
                {
                    Description = "Удалена роль Patron",
                    StatusCode = 200
                };
            }
            catch {
                return new BaseResponse<Patron>
                {
                    Description = "Не удалось удалить роль Patron"
                };
            }
        }

        public async Task<BaseResponse<Patron>> GetAsync(int id) {
            Patron patron = await patronRepository.GetAsync(id);
            if (patron == null) {
                return new BaseResponse<Patron>
                {
                    Description="Пользователь не найден"
                };
            }
            return new BaseResponse<Patron>
            {
                Data=patron,
                StatusCode = 200
            };
        }
    }
}
