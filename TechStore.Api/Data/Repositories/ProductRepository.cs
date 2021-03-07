using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using TechStore.Api.Data.Enteties;

namespace TechStore.Api.Data.Repositories
{
    public class ProductRepository : GenericRepository<Product>
    {
        public ProductRepository
            (
                TechStoreDbContext _dbContext
            )
            : base (_dbContext)
        {

        }

        public override Product Update(Product entity)
        {
            var product = _dbContext.Products
                .Single(p => p.Id == entity.Id);

            return base.Update(entity);
        }

        //public override IEnumerable<Product> Find(Expression<Func<Product, bool>> predicate)
        //{
        //    return _dbContext.Product
        //        .Where(predicate);
        //}
    }
}
