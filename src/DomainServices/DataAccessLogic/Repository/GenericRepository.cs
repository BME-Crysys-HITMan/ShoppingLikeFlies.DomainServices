using Microsoft.EntityFrameworkCore;
using ShoppingLikeFiles.DataAccessLogic.Context;
using ShoppingLikeFiles.DataAccessLogic.Entities;
using System.Linq.Expressions;

namespace ShoppingLikeFiles.DataAccessLogic.Repository;

internal class GenericRepository<T> : IGenericRepository<T>, IDisposable where T : EntityBase
{
    private readonly ShoppingLikeFliesDbContext _dbContext;
    private readonly DbSet<T> _entitiySet;


    public GenericRepository(ShoppingLikeFliesDbContext dbContext)
    {
        _dbContext = dbContext;
        _entitiySet = _dbContext.Set<T>();
    }

    public async Task<int> AddAsync(T entity)
    {
        var e = await _dbContext.AddAsync(entity);
        await SaveChangesAsync();
        return e.Entity.Id;
    }

    public async Task AddRangeAsync(IEnumerable<T> entities)
    {
        await _dbContext.AddRangeAsync(entities);
        await SaveChangesAsync();
    }


    public async Task<IEnumerable<T>> GetAllAsync()
    {
        var entities = await _entitiySet.ToListAsync();

        return entities;
    }

    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression)
        => await _entitiySet.Where(expression).ToListAsync();

    public Task<T?> GetAsync(Expression<Func<T, bool>> expression)
        => _entitiySet.FirstOrDefaultAsync(expression);

    public async Task<T?> GetAsync(int id)
    {
        var e = await _entitiySet.FindAsync(id);

        return e;
    }

    public IQueryable<T> GetQueryable() => _entitiySet;

    public async Task RemoveAsync(T entity)
    {
        _dbContext.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> RemoveAsync(int id)
    {
        var entity = await GetAsync(id);
        if (entity == null)
            return false;
        await RemoveAsync(entity);
        return true;
    }

    public async Task RemoveRangeAsync(IEnumerable<T> entities)
    {
        _dbContext.RemoveRange(entities);
        await SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        _dbContext.Update(entity);
        await SaveChangesAsync();
    }

    public async Task UpdateRangeAsync(IEnumerable<T> entities)
    {
        _dbContext.UpdateRange(entities);
        await SaveChangesAsync();
    }

    protected Task<int> SaveChangesAsync()
    {
        var entries = _dbContext.ChangeTracker.Entries();

        foreach (var entry in entries)
        {
            if (entry.Entity is EntityBase && (entry.State == EntityState.Modified || entry.State == EntityState.Added))
            {
                var time = DateTime.UtcNow;
                if (entry.State == EntityState.Added)
                    (entry.Entity as EntityBase).Created = time;
                (entry.Entity as EntityBase).Updated = time;
            }
        }

        return _dbContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        _dbContext.SaveChanges();
        _dbContext.Dispose();
    }
}
