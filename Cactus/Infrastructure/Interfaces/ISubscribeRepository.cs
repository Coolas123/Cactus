﻿using Cactus.Models.Database;

namespace Cactus.Infrastructure.Interfaces
{
    public interface ISubscribeRepository:IBaseRepository<AuthorSubscribe>
    {
        Task<AuthorSubscribe> GetSubscriptionAsync(int subcriberId, int authorId);
        Task<IEnumerable<AuthorSubscribe>> GetPagingSubscribersAsync(int subcriberId, int authorPage, int pageSize);
        Task<IEnumerable<AuthorSubscribe>> GetSubscribersAsync(int subcriberId);
    }
}
