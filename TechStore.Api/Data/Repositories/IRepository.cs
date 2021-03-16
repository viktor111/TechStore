using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TechStore.Api.Data.Enteties;
using TechStore.Models.Models;

namespace TechStore.Api.Data.Repositories
{
    public interface IRepository<T>
    {
        Task<T> Add(T entity);

        Task<T> Update(T entity);

        Task<T> Get(int id, bool include = false);

        Task<T> GetByProperty(Expression<Func<T, bool>> predicate);

        Task<T> Delete(T entity);

        Task<IEnumerable<T>> All();

        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);

        Task<T> AddToCart(T product, Cart cart);

        Task<bool> SaveChanges();

        Task<AuthenticateResponse> Authenticate(T model);

        Task<List<T>> GetListByProperty(Expression<Func<T, bool>> predicate);

        Task<IEnumerable<T>> PagedList(PagedParameters parameters);
    }
}
