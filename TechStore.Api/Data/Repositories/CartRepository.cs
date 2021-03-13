using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechStore.Api.Data.Enteties;

namespace TechStore.Api.Data.Repositories
{
    public class CartRepository : GenericRepository<Cart>
    {
        public CartRepository
            (
                TechStoreDbContext _dbContext
            )
            : base(_dbContext)
        {
        }

        public async override Task<Cart> Get(int id, bool include)
        {
            var result = await _dbContext.Carts.FirstOrDefaultAsync(c => c.Id == id);

            return result;
        }
    }
}
