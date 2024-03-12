﻿using Cactus.Models.Database;
using Cactus.Models.Responses;

namespace Cactus.Services.Interfaces
{
    public interface IDonatorService
    {
        Task<BaseResponse<Donator>> GetDonator(int targetId, int typeId, int userId);
        Task<BaseResponse<Dictionary<int, decimal>>> GetCollectedSumOfGoals(List<int> optionId);

    }
}
