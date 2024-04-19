using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StockRepositoryLayer.Data
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _dbContext;
        private readonly DbSet<T> entity;
        public Repository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            entity= _dbContext.Set<T>();
        }

        public async Task<T> AddAsync(T entity)
        {
             await _dbContext.AddAsync(entity);
            if (await SaveAsync())
            {
                return entity;
            }
            return null;
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter)
        {
           return await entity.Where(filter).ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await entity.FindAsync(id);
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter)
        {
            if (filter == null)
            {
                throw new Exception("filter is required");
            }
            return await entity.FirstOrDefaultAsync(filter);
        }

        public async Task Remove(int id)
        {
            try
            {
                var removedEntity = await GetByIdAsync(id);
                entity.Remove(removedEntity);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
          
        }

        public async Task<bool> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync()>0;
        }

        public async Task<T> Update(T UpdatedEntity)
        {
            entity.Update(UpdatedEntity);
            if(await SaveAsync()) 
            {
                return UpdatedEntity;
            }
            return null;
        }
    }
}
