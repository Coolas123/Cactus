﻿using Cactus.Models.Database;
using Cactus.Models.Responses;

namespace Cactus.Services.Interfaces
{
    public interface IPostDonationOptionService
    {
        Task<BaseResponse<DonationOption>> GetOption(int postId);
        Task<BaseResponse<bool>> AddOptionToPostAsync(int postId, int optionId);
    }
}