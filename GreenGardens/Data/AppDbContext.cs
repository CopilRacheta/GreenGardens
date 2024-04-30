using Microsoft.EntityFrameworkCore;
using GreenGardens.Model;

namespace GreenGardens.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<ProductModel> Products { get; set; }

        public DbSet<BasketModel> Basket { get; set; }

        public DbSet<CustomersModel> Customers { get; set; }
        public DbSet<OrderModel> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public object Items { get; internal set; }


    }
}
