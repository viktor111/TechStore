using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TechStore.Api.Data.Enteties;

namespace TechStore.Api
{
    public class TechStoreDbContext : DbContext
    {
        private readonly IConfiguration _config;

        public TechStoreDbContext(DbContextOptions options, IConfiguration config) : base(options)
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config.GetConnectionString("Default"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        public DbSet<Product> Product { get; set; }
    }
}
