using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GreenGardens.Model;
using GreenGardens.Data;
using Microsoft.EntityFrameworkCore;

namespace GreenGardens.Pages
{
    public class ShopModel : PageModel
    {

        private readonly AppDbContext _db;

        public ShopModel(AppDbContext db)
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
            var customerId = HttpContext.Session.GetString("CustomerId");
            if (string.IsNullOrEmpty(customerId))
            {
                return RedirectToPage("/Login");
            }

            var productToAdd = await _db.Products.FindAsync(productId);
            if (productToAdd == null)
            {
                return NotFound();
            }

            //var basketItem = new BasketModel { ProductId = productToAdd.ProductId, CustomerId = customerId };
            //_db.Baskets.Add(basketItem);
            await _db.SaveChangesAsync();
            return RedirectToPage(); // Refresh the page or redirect to a confirmation page
        }

    }
}
