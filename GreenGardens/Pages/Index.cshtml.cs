using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GreenGardens.Model;
using GreenGardens.Data;

namespace GreenGardens.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public string Username { get; set; }

        public List<OrderModel> Order { get; set; }

        public List<CustomersModel> Customers { get; set; }

        public List<ProductModel> Products { get; set; }

        private readonly AppDbContext _dbConnection;
        public IndexModel(ILogger<IndexModel> logger , AppDbContext _db)
        {
            _logger = logger;

            _dbConnection = _db;
        }

        public void OnGet()
        {
            Order = _dbConnection.Orders.ToList();
            Customers = _dbConnection.Customers.ToList();
            Products = _dbConnection.Products.ToList();

            Username = "DefaultUser";
        }
    }
}