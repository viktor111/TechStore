using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TechStore.Api.Data.Repositories
{
    public abstract class GenericRepository<T> :
        IRepository<T> where T : class
    {
        protected TechStoreDbContext _dbContext;

        public GenericRepository(TechStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async virtual Task<T> Add(T entity)
        {
            var result = await _dbContext.AddAsync(entity);

            return result.Entity;
        }

        public virtual IEnumerable<T> All()
        {
            return _dbContext.Set<T>()
                .ToList();
        }

        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>()
                .AsQueryable()
                .Where(predicate)
                .ToList();
        }

        public async virtual Task<T> Get(int id)
        {
            return await _dbContext.FindAsync<T>(id);
        }

        public async virtual Task<bool> SaveChanges()
        {
            return (await _dbContext.SaveChangesAsync()) > 0;
        }

        public virtual T Update(T entity)
        {
            return _dbContext.Update(entity).Entity;
        }
    }
}
