using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TechStore.Api.Data.Enteties;

namespace TechStore.Api.Data.Repositories
{
    public class UserRepository : GenericRepository<User>
    {
        public UserRepository
            (
                TechStoreDbContext _dbContext
            )
            : base(_dbContext)
        {
        }

        public async override Task<User> Get(int id, bool include)
        {
            if (include)
            {
                var reslutWithInclude = await _dbContext.Users
                    .Where(u => u.Id == id)
                    .Include(x => x.Cart)
                    .FirstOrDefaultAsync();

                return reslutWithInclude;
            }
            var result = await _dbContext.Users
                .Where(u => u.Id == id)
                .FirstOrDefaultAsync();

            return result;
        }
    }
}
