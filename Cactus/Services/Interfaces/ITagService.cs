﻿using Cactus.Models.Database;
using Cactus.Models.Responses;

namespace Cactus.Services.Interfaces
{
    public interface ITagService
    {
        Task<BaseResponse<IEnumerable<Tag>>> GetAll();
        Task<BaseResponse<IEnumerable<Tag>>> GetAllByNames(IEnumerable<string> tags);
        Task<BaseResponse<bool>> CreateAsync(IEnumerable<string> tags);
    }
}
