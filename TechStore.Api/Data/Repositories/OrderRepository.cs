using System;
using TechStore.Api.Data.Enteties;

namespace TechStore.Api.Data.Repositories
{
    public class OrderRepository : GenericRepository<Order>
    {
        public OrderRepository
            (
                TechStoreDbContext _dbContext
            )
            : base(_dbContext)
        {
        }


    }
}
