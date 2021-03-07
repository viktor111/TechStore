using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

        public async override Task<Product> Get(int id, bool include)
        {
            if (include)
            {
                var resultInclude = await _dbContext.Products
                    .Where(p => p.Id == id)
                    .Include(p => p.CartProduct)
                    .FirstOrDefaultAsync();

                return resultInclude;
            }

            var result = await _dbContext.Products
                    .FirstOrDefaultAsync(p => p.Id == id);

            return result;
        }

        public async override Task<Product> Update(Product entity)
        {
            var product = await _dbContext.Products
                .SingleAsync(p => p.Id == entity.Id);

            return product;
        }

        public async override Task<Product> GetByProperty(Expression<Func<Product, bool>> predicate)
        {

            var result = await _dbContext.Products.FirstOrDefaultAsync(predicate);

            return result;
        }

        public async override Task<Product> Delete(Product product)
        {
            var cartProducts = await _dbContext.CartProducts
                .Where(cp => cp.ProductId == product.Id)
                .ToListAsync();

            _dbContext.CartProducts
                .RemoveRange(cartProducts);

            _dbContext.Products
                .Remove(product);

            return product;
        }

        public async override Task<Product> AddToCart(Product product, Cart cart)
        {
            var cartProduct = product.CartProduct.FirstOrDefault(x => x.CartId == cart.Id);

            if (cartProduct is not null)
            {
                cartProduct.Quantity = cartProduct.Quantity + 1;
            }
            else
            {
                product.CartProduct.Add(new CartProduct { CartId = cart.Id });
            }

            return product;
        }


        //public override IEnumerable<Product> Find(Expression<Func<Product, bool>> predicate)
        //{
        //    return _dbContext.Product
        //        .Where(predicate);
        //}
    }
}
