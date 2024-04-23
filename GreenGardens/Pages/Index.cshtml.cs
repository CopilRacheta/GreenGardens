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

        public List<OrderModel> Items { get; set; }

        private readonly AppDbContext _dbConnection;
        public IndexModel(ILogger<IndexModel> logger , AppDbContext _db)
        {
            _logger = logger;

            _dbConnection = _db;
        }

        public void OnGet()
        {
            Items = _dbConnection.Order.ToList();

            Username = "DefaultUser";
        }
    }
}