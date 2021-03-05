using System;
using System.Collections;
using System.Collections.Generic;

namespace TechStore.Data.Repositories
{
    public interface IRepository<T>
    {
        T Add(T entity);

        T Update(T entity);

        T Get(T entity);

        IEnumerable<T> All(T entity);

        IEnumerable<T> Find(T entity);

        void SaveChanges();
    }
}
