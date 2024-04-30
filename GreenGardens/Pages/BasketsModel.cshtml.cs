using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GreenGardens.Data;
using GreenGardens.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http; // Add this namespace for HttpContext.Session


namespace GreenGardens.Pages
{
    public class BasketsModel : PageModel
    {
        private readonly AppDbContext _db;

        public BasketsModel(AppDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public ProductModel Product { get; set; }

        public List<ProductModel> Products { get; set; }

        public void OnGet()
        {
            Products = _db.Products.ToList();
        }

        public async Task<IActionResult> OnPostAddToBasketAsync(int productId)
        {
            var emailAdress = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(emailAdress))
            {
                return RedirectToPage("/Login");
            }

            var productToAdd = await _db.Products.FindAsync(productId);
            if (productToAdd == null)
            {
                return NotFound();
            }

            // Instantiate BasketModel with required parameters
            var basketItem = new BasketModel
            {
                ProductId = productToAdd.Id,
                EmailAddress = emailAdress
            };

            // Add the basket item to the database context and save changes
            _db.Basket.Add(basketItem);
            await _db.SaveChangesAsync();

            // Redirect to the current page (refresh)
            return RedirectToPage();
        }

    }
}

