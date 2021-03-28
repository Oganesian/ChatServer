using AccountAndConnection;
using ChatClient.DataBaseConnection;
using ChatClient.Factories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatClient.Services.DataServices
{
    public class GenericDataService<T> : IDataService<T> where T : DomainObject
    {
        private readonly ChatClientDbContextFactory _contextFactory;

        public GenericDataService(ChatClientDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<T> Create(T entity)
        {
            using DbContextBase dbContext = _contextFactory.CreateDbContext();

            var newEntity = await dbContext.Set<T>().AddAsync(entity);
            await dbContext.SaveChangesAsync();

            return newEntity.Entity;
        }

        public async Task<bool> Delete(int id)
        {
            using DbContextBase dbContext = _contextFactory.CreateDbContext();

            T entity = await dbContext.Set<T>().FirstOrDefaultAsync((e) => e.Id == id);
            dbContext.Set<T>().Remove(entity);
            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            using DbContextBase dbContext = _contextFactory.CreateDbContext();

            return await dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            using DbContextBase dbContext = _contextFactory.CreateDbContext();

            return await dbContext.Set<T>().FirstOrDefaultAsync((e) => e.Id == id);
        }

        public async Task<T> Update(int id, T entity)
        {
            using DbContextBase dbContext = _contextFactory.CreateDbContext();

            entity.Id = id;
            dbContext.Set<T>().Update(entity);
            await dbContext.SaveChangesAsync();

            return entity;
        }
    }
}
