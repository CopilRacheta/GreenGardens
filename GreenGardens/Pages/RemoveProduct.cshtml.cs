using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using System.Linq;
using GreenGardens.Model;
using GreenGardens.Data;

namespace GreenGardens.Pages
{
    public class RemoveProductModel : PageModel
    {

        private readonly AppDbContext _dbConnection;

        public ProductModel Product { get; set; }

        public RemoveProductModel(AppDbContext context)
        {
            _dbConnection = context;
        }
        public void OnGet(int id)
        {
            Product = _dbConnection.Products.FirstOrDefault(p => p.ProductId == id);
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var productToDelete = _dbConnection.Products.FirstOrDefault(t=> t.ProductId ==id);
            if (productToDelete != null)
            {
                _dbConnection.Products.Remove(productToDelete);
                await _dbConnection.SaveChangesAsync();
                return RedirectToPage("Products");
            }
            else
            {
                return NotFound();
            }
        }

    }
}
