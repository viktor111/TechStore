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
            modelBuilder.Entity<CartProduct>()
                .Property("Quantity")
                .HasDefaultValue(1);

            //One To One with User and Cart
            modelBuilder.Entity<Cart>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Cart>()
                .HasOne(c => c.User)
                .WithOne(u => u.Cart)
                .HasForeignKey<User>(u => u.CartId);                          

            // Many to Many with Cart and Product
            modelBuilder.Entity<CartProduct>()
                .HasKey(e => new { e.CartId, e.ProductId });

            modelBuilder.Entity<CartProduct>()
                .HasOne(t => t.Cart)
                .WithMany(t => t.CartProduct)
                .HasForeignKey(t => t.CartId);

            modelBuilder.Entity<CartProduct>()
                .HasOne(t => t.Product)
                .WithMany(t => t.CartProduct)
                .HasForeignKey(t => t.ProductId);            

        }

        public DbSet<User> Users { get; set; }
        public DbSet<CartProduct> CartProducts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts{ get; set; }
    }
}
