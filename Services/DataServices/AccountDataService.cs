using AccountAndConnection;
using Microsoft.EntityFrameworkCore;
using Services.DataBaseConnection;
using Services.Factories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.DataServices
{
    public class AccountDataService : IAccountDataService
    {
        private readonly ChatClientDbContextFactory _contextFactory;

        public AccountDataService(ChatClientDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<BaseAccount> Create(BaseAccount entity)
        {
            using DbContextBase dbContext = _contextFactory.CreateDbContext();

            var newEntity = await dbContext.Accounts.AddAsync(entity);
            await dbContext.SaveChangesAsync();

            return newEntity.Entity;
        }

        public async Task<bool> Delete(int id)
        {
            using DbContextBase dbContext = _contextFactory.CreateDbContext();

            BaseAccount entity = await dbContext.Accounts.FirstOrDefaultAsync((e) => e.Id == id);
            dbContext.Accounts.Remove(entity);
            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<BaseAccount>> GetAll()
        {
            using DbContextBase dbContext = _contextFactory.CreateDbContext();

            return await dbContext.Accounts.ToListAsync();
        }

        public async Task<BaseAccount> GetByEmail(string email)
        {
            using DbContextBase dbContext = _contextFactory.CreateDbContext();

            return await dbContext.Accounts.FirstOrDefaultAsync((e) => e.Email == email);
        }

        public async Task<BaseAccount> GetById(int id)
        {
            using DbContextBase dbContext = _contextFactory.CreateDbContext();

            return await dbContext.Accounts.FirstOrDefaultAsync((e) => e.Id == id);
        }

        public async Task<BaseAccount> GetByUsername(string username)
        {
            using DbContextBase dbContext = _contextFactory.CreateDbContext();

            return await dbContext.Accounts.FirstOrDefaultAsync((e) => e.Username == username);
        }

        public async Task<BaseAccount> Update(int id, BaseAccount entity)
        {
            using DbContextBase dbContext = _contextFactory.CreateDbContext();

            entity.Id = id;
            dbContext.Accounts.Update(entity);
            await dbContext.SaveChangesAsync();

            return entity;
        }
    }
}
