using ShoppingLikeFiles.DataAccessLogic.Entities;
using System.Linq.Expressions;

namespace ShoppingLikeFiles.DataAccessLogic.Repository;

public interface IGenericRepository<T> : IDisposable where T : EntityBase
{
    IQueryable<T> GetQueryable();
    Task<bool> RemoveAsync(int id);
    Task RemoveAsync(T entity);
    Task RemoveRangeAsync(IEnumerable<T> entities);
    Task UpdateAsync(T entity);
    Task UpdateRangeAsync(IEnumerable<T> entities);
    Task<T?> GetAsync(int id);
    Task<T?> GetAsync(Expression<Func<T, bool>> expression);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression);
    Task<int> AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> entities);
}
