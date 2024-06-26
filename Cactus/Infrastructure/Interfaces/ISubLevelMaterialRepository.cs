﻿using Cactus.Models.Database;

namespace Cactus.Infrastructure.Interfaces
{
    public interface ISubLevelMaterialRepository:IBaseRepository<SubLevelMaterial>
    {
        Task<bool> UpdateCoverAsync(SubLevelMaterial entity);
        Task<IEnumerable<SubLevelMaterial>> GetMaterialsAsync(int authorId);
    }
}
