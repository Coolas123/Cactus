﻿using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Microsoft.EntityFrameworkCore;
using Nest;

namespace Cactus.Infrastructure.Repositories
{
    public class PayMethodSettingRepository : IPayMethodSettingRepository
    {
        private readonly ApplicationDbContext dbContext;
        public PayMethodSettingRepository(ApplicationDbContext dbContext) {
            this.dbContext = dbContext;
        }
        public async Task<bool> CreateAsync(PayMethodSetting entity) {
            await dbContext.PayMethodSettings.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public Task<bool> DeleteAsync(PayMethodSetting entity) {
            throw new NotImplementedException();
        }

        public async Task<PayMethodSetting> GetAsync(int id) {
            return await dbContext.PayMethodSettings.FirstOrDefaultAsync(x=>x.Id==id);
        }

        public async Task<PayMethodSetting> GetIntrasystemOperationsSettingAsync() {
            return await dbContext.PayMethodSettings.FirstOrDefaultAsync(x => x.TransactionTypeId==(int)Models.Enums.TransactionType.IntrasystemOperations);
        }

        public Task<IEnumerable<PayMethodSetting>> SelectAsync() {
            throw new NotImplementedException();
        }
    }
}
