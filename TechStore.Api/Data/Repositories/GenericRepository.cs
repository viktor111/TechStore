using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TechStore.Api.Data.Enteties;
using TechStore.Models.Models;

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
            await SaveChanges();
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

        public async virtual Task<T> GetByProperty(Expression<Func<T, bool>> predicate)
        {
            var genericDb = _dbContext.Set<T>();

            var result = await genericDb.FirstOrDefaultAsync(predicate);

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

        public async virtual Task<T> Update(T entity)
        {
            var result = _dbContext.Update(entity).Entity;
            //await SaveChanges();
            return result;
        }

        public virtual Task<T> Delete(T entity)
        {
            _dbContext.Remove(entity);

            return Task.FromResult(entity);
        }

        public async virtual Task<T> AddToCart(T product, Cart cart)
        {
            return product;
        }

        public async virtual Task<AuthenticateResponse> Authenticate(T model)
        {
            return new AuthenticateResponse(new UserModel(), "");
        }

        public async virtual Task<List<T>> GetListByProperty(Expression<Func<T, bool>> predicate)
        {
            var genericDb = await _dbContext.Set<T>().Where(predicate).ToListAsync();

            return genericDb;
        }
    }
}
