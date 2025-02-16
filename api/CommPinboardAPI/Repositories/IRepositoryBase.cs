using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CommPinboardAPI.Repositories
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, Expression<Func<T, object>> order = null, bool isAsc = true, bool isTracked = true, Expression<Func<T, object>> include = null, Expression<Func<T, object>> include2 = null);
        Task<T> GetAsync(Expression<Func<T, bool>> filter = null, Expression<Func<T, object>> order = null, bool isAsc = true, bool isTracked = true, Expression<Func<T, object>> include = null);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T oldEntity, T newEntity);
    }
}