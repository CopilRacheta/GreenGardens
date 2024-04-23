using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GreenGardens.Model;
using GreenGardens.Data;

namespace GreenGardens.Pages
{
    public class ProductsModel : PageModel
    {

        private readonly AppDbContext _db;

        public List<ProductModel> Products { get; set; }

        public ProductsModel (AppDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
            Products = _db.Products.ToList();
        }
    }
}
