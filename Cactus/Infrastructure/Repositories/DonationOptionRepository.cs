﻿using Cactus.Infrastructure.Interfaces;
using Cactus.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace Cactus.Infrastructure.Repositories
{
    public class DonationOptionRepository : IDonationOptionRepository
    {
        private readonly ApplicationDbContext dbContext;
        public DonationOptionRepository(ApplicationDbContext dbContext) {
            this.dbContext = dbContext;
        }
        public async Task<bool> CreateAsync(DonationOption entity) {
            await dbContext.DonationOptions.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public Task<bool> DeleteAsync(DonationOption entity) {
            throw new NotImplementedException();
        }

        public async Task<DonationOption> GetAsync(int id) {
            return await dbContext.DonationOptions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<DonationOption> GetByPriceAsync(decimal price) {
            return await dbContext.DonationOptions.FirstOrDefaultAsync(x=>x.Price== price);
        }

        public async Task<DonationOption> GetLastAsync(int authorId) {
            return await dbContext.DonationOptions.OrderBy(x=>x.Id).LastOrDefaultAsync(x=>x.AuthorId== authorId);
        }

        public async Task<IEnumerable<DonationOption>> GetOptionsAsync(int authorId) {
            return await dbContext.DonationOptions.Where(x=>x.AuthorId== authorId).ToListAsync();
        }

        public Task<IEnumerable<DonationOption>> SelectAsync() {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(DonationOption entity) {
            dbContext.Update(entity);
            await dbContext.SaveChangesAsync();
            return true;
        }
    }
}
