using DataAccessLogic.Context;
using DataAccessLogic.Entities;
using DataAccessLogic.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLogic.Repository
{
    internal class GenericRepository<T> : IGenericRepository<T> where T : EntityBase<T>, new()
    {
        private readonly ShoppingLikeFliesDbContext _dbContext;
        private readonly DbSet<T> _entitiySet;


        public GenericRepository(ShoppingLikeFliesDbContext dbContext)
        {
            _dbContext = dbContext;
            _entitiySet = _dbContext.Set<T>();
        }

        public async Task AddAsync(T entity)
            => await _dbContext.AddAsync(entity);

        public async Task AddRangeAsync(IEnumerable<T> entities)
            => await _dbContext.AddRangeAsync(entities);

        public async Task<IEnumerable<T>> GetAllAsync()
            => await _entitiySet.IncludeAll().ToListAsync();

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression)
            => await _entitiySet.IncludeAll().Where(expression).ToListAsync();

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression)
            => await _entitiySet.IncludeAll().FirstOrDefaultAsync(expression);

        public async Task<T> GetAsync(int id)
            => await _entitiySet.IncludeAll().SingleAsync(x => x.Id == id);

        public IQueryable<T> GetQueryable() => _entitiySet;

        public async Task RemoveAsync(T entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            var entity = await GetAsync(id);
            await RemoveAsync(entity);
        }

        public async Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            _dbContext.RemoveRange(entities);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateRangeAsync(IEnumerable<T> entities)
        {
            _dbContext.UpdateRange(entities);
            await _dbContext.SaveChangesAsync();
        }
    }
}
