using Microsoft.EntityFrameworkCore;
using GreenGardens.Model;

namespace GreenGardens.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<OrderModel> Order { get; set; }

        public DbSet<CustomersModel> Customers { get; set; }

        public DbSet<ProductModel> Products { get; set; }

        public object Items { get; internal set; }

        public AppDbContext(DbContextOptions<AppDbContext>options) : base(options)
        {

        }
    }
}
