using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;
using Cactus.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Nest;

namespace Cactus.Services.Implementations
{
    public class TransactionService: ITransactionService
    {
        private readonly ITransactionRepository transactionRepository;
        public TransactionService(ITransactionRepository transactionRepository) {
            this.transactionRepository = transactionRepository;
        }

        public async Task<BaseResponse<bool>> CreateTransaction(TransactionViewModel model) {
            var transaction = new Transaction
            {
                UserId = model.UserId,
                PayMethodId = model.PayMethodId,
                Sended = model.Sended,
                Received = model.Received,
                Created = model.Created.ToUniversalTime(),
                StatusId = (int)Models.Enums.TransactionStatus.Received
            };
            try {
                await transactionRepository.CreateAsync(transaction);
            }
            catch {
                return new BaseResponse<bool>
                {
                    Description="Не удалось провести транзакцию"
                };
            }
            return new BaseResponse<bool>
            {
                Data=true,
                StatusCode=200
            };
        }
    }
}
