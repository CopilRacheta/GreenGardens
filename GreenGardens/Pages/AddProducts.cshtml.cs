using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GreenGardens.Model;
using GreenGardens.Data;

namespace GreenGardens.Pages
{
    public class AddProductsModel : PageModel
    {

        private readonly AppDbContext _dbConnection;

        public ProductModel Product { get; set; }

        public AddProductsModel(AppDbContext context)
        {
            _dbConnection = context;
        }
        public void OnGet()
        {
            Product = new ProductModel();   
        }

        public IActionResult OnPost(ProductModel Product)
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            _dbConnection.Products.Add(Product);
            _dbConnection.SaveChanges();
            return RedirectToPage("Products");
        }
    }
}
