using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Database.Common.Contexts
{
    public interface IMongoContext<T> : IDisposable
        
    {
        Task AddAsync(T entity);
        Task AddManyAsync(IEnumerable<T> entities);
        Task DeleteWhereAsync(Expression<Func<T, bool>> where);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetSingleAsync(Expression<Func<T, bool>> where);
        Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> where);
    }
}
