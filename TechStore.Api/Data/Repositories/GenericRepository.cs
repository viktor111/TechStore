using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace TechStore.Api.Data.Repositories
{
    public abstract class GenericRepository<T> :
        IRepository<T> where T : class
    {
        protected TechStoreDbContext _dbContext;

        public GenericRepository
            (
                TechStoreDbContext dbContext
            )
        {
            _dbContext = dbContext;
        }

        public async virtual Task<T> Add(T entity)
        {
            var result = await _dbContext.AddAsync(entity);

            return result.Entity;
        }

        public async virtual Task<IEnumerable<T>> All()
        {
            var result = await _dbContext.Set<T>()
                .ToListAsync();

            return result;
        }

        public async virtual Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
        {
            var result = await _dbContext.Set<T>()
                .AsQueryable()
                .Where(predicate)
                .ToListAsync();

            return result;
        }

        public async virtual Task<T> Get(int id, bool include)
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
