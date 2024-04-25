using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using GreenGardens.Data;
using GreenGardens.Model;

namespace GreenGardens.Graphs
{
    public class GraphsModel : PageModel
    {
        private readonly AppDbContext _dbConnection;

        public string ProductsJson { get; set; }

        public GraphsModel(AppDbContext db)
        {
            _dbConnection = db;
        }



        public void OnGet()
        {
            var products = _dbConnection.Products.ToList();
            ProductsJson = JsonSerializer.Serialize(products.Select(t=> new {t.Name ,t.Stock, t.ExpectedStock ,t.Price , t.LoyalityPoints}));

        }
    }
}
