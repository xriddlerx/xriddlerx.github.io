using Microsoft.EntityFrameworkCore;
using ProjekatDataAccess.Configurations;
using ProjekatDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatDataAccess
{
    public class ProjekatContext : DbContext
    {
        private readonly string _connectionString;
        public ProjekatContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ProjekatContext()
        {
            _connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=projekatAsp;TrustServerCertificate=true;Integrated security = true";
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BrandConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new BrandCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new GalleryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new PriceConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new CartConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new ProductCartConfiguration());
            modelBuilder.ApplyConfiguration(new ProductOrderConfiguration());
            modelBuilder.ApplyConfiguration(new UseCaseLogConfiguration());


            modelBuilder.Entity<ProductCart>().HasKey(x => new { x.CartId, x.ProductId});
            modelBuilder.Entity<ProductOrder>().HasKey(x => new { x.OrderId, x.ProductId});
            modelBuilder.Entity<UserUseCase>().HasKey(x => new { x.UserId, x.UseCaseId});
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BrandCategory> BrandCategory { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductCart> ProductCart { get; set; }
        public DbSet<ProductOrder> ProductOrder { get; set; }
        public DbSet<UserUseCase> UserUseCases { get; set; }
        public DbSet<UseCaseLog> UseCaseLogs { get; set; }

    }
}
