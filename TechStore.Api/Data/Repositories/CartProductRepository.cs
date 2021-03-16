using System;
using TechStore.Api.Data.Enteties;

namespace TechStore.Api.Data.Repositories
{
    public class CartProductRepository : GenericRepository<CartProduct>
    {
        public CartProductRepository
            (
                TechStoreDbContext _dbContext
            )
            : base(_dbContext)
        {
        }


    }
}
