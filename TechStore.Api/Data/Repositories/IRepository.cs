using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TechStore.Api.Data.Repositories
{
    public interface IRepository<T>
    {
        Task<T> Add(T entity);

        T Update(T entity);

        Task<T> Get(int id, bool include = false);

        Task<IEnumerable<T>> All();

        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);

        Task<bool> SaveChanges();
    }
}
