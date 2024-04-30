using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System.Threading.Tasks;
using GreenGardens.Model;
using GreenGardens.Data;

namespace GreenGardens.Pages
{
    public class EditProductModel : PageModel
    {
        private readonly AppDbContext _dbConnection;

        [BindProperty]
        public ProductModel Product { get; set; }

        public EditProductModel (AppDbContext context)
        {
            _dbConnection = context;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Product = await _dbConnection.Products.FindAsync(id);

            if(Product == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult>OnPostAsync()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            var productToUpdate = await _dbConnection.Products.FindAsync(Product.Id);

            if(productToUpdate == null)
            {
                return NotFound();
            }

            productToUpdate.Name = Product.Name;
            productToUpdate.Description = Product.Description;
            productToUpdate.Price = Product.Price;
            productToUpdate.Stock = Product.Stock;
            productToUpdate.ExpectedStock = Product.ExpectedStock;
            productToUpdate.LoyalityPoints = Product.LoyalityPoints;

            await _dbConnection.SaveChangesAsync();

            return RedirectToPage("Products");
        }
    }
}
