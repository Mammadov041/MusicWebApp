using MusicWebApp.Core.Abstract;
using System.Linq.Expressions;

namespace MusicWebApp.Core.DataAccess.Abstract
{
    public interface IEntityRepository<T> 
        where T : class, IEntity, new()
    {
        Task<T> GetAsync(Expression<Func<T, bool>> filter);
        Task<List<T>> GetListAsync(Expression<Func<T, bool>> filter = null);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
