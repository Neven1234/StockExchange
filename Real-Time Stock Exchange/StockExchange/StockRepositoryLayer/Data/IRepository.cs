using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StockRepositoryLayer.Data
{
    public interface IRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);
        Task<T>GetByIdAsync(int id);
        Task Remove(int id);
        Task<T> Update(T entity);
        Task<T> GetAsync(Expression<Func<T, bool>> filter);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter);
         Task<bool> SaveAsync();
    }
}
