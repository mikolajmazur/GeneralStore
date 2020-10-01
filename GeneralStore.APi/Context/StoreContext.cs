using GeneralStore.Api.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneralStore.Api.Context
{
    public class StoreContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public StoreContext(DbContextOptions<StoreContext> options) 
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // manufacturers
            modelBuilder.Entity<Manufacturer>().HasData(
                new Manufacturer 
                { 
                    Id = new Guid("42727D34-66D1-41DA-9F5B-6991B869D140"),
                    Name = "Timmy" 
                },
                new Manufacturer 
                { 
                    Id = new Guid("6CED6BAF-1DA9-499B-96C2-DB3F6E3CF062"),
                    Name = "Papierex" 
                }
                );

            // categories
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = new Guid("8C02B200-10DE-40A7-A1C5-E963947F5697"),
                    Name = "Toys"
                },
                new Category
                {
                    Id = new Guid("EF001E3F-472D-46B1-A8F8-32A15EBBC78B"),
                    Name = "Paper products"
                }
                );

            // products
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = new Guid("9E64A226-43DF-487E-AEAE-883F40CAB282"),
                    Name = "Toy car",
                    Description = "Very good, very fast",
                    Price = 120.0m,
                    ManufacturerId = new Guid("42727D34-66D1-41DA-9F5B-6991B869D140"),
                    CategoryId = new Guid("8C02B200-10DE-40A7-A1C5-E963947F5697"),
                    AvailableUnits = 100
                },
                new Product 
                {
                    Id = new Guid("4864203A-F343-4F51-AD6A-EDEFFE2BC77C"),
                    Name = "Doll",
                    Description = "Very pretty doll",
                    Price = 15.5m,
                    ManufacturerId = new Guid("42727D34-66D1-41DA-9F5B-6991B869D140"),
                    CategoryId = new Guid("8C02B200-10DE-40A7-A1C5-E963947F5697"),
                    AvailableUnits = 100
                },
                new Product 
                {
                    Id = new Guid("ACB091E9-3440-4E1B-8C72-3F7D07FFDB45"),
                    Name = "500 paper sheets",
                    Description = "White paper for printers",
                    Price = 40m,
                    ManufacturerId = new Guid("6CED6BAF-1DA9-499B-96C2-DB3F6E3CF062"),
                    CategoryId = new Guid("EF001E3F-472D-46B1-A8F8-32A15EBBC78B"),
                    AvailableUnits = 100
                },
                new Product
                {
                    Id = new Guid("E8AAB2B0-3092-4B63-A128-41730F06CB80"),
                    Name = "Pretty A4 notepad",
                    Description = "Pretty notepad with 60 pages",
                    Price = 20m,
                    ManufacturerId = new Guid("6CED6BAF-1DA9-499B-96C2-DB3F6E3CF062"),
                    CategoryId = new Guid("EF001E3F-472D-46B1-A8F8-32A15EBBC78B"),
                    AvailableUnits = 100
                },
                new Product
                {
                    Id = new Guid("53CE0364-8501-447D-953B-5A24F9A99A8D"),
                    Name = "Ugly, but cheap A4 notepad",
                    Description = "Cheap A4 notepad, 50 pages",
                    Price = 8m,
                    ManufacturerId = new Guid("6CED6BAF-1DA9-499B-96C2-DB3F6E3CF062"),
                    CategoryId = new Guid("EF001E3F-472D-46B1-A8F8-32A15EBBC78B"),
                    AvailableUnits = 100
                });
        }
    }
}
