using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GreenGardens.Model;
using GreenGardens.Data;

namespace GreenGardens.Pages
{
    public class Basket : PageModel
    {
        private readonly AppDbContext _db;

        public Basket(AppDbContext db)
        {
            _db = db;
        }

        public List<BasketProductViewModel> BasketItems { get; set; }

        public class BasketProductViewModel
        {
            public int BasketId { get; set; }
            public int Quantity { get; set; }

            public string ProductName { get; set; }

            public string Description { get; set; }

        }

        public async Task OnGetAsync()
        {
            string emailAdress = HttpContext.Session.GetString("UserId");

            if (!string.IsNullOrEmpty(emailAdress))
            {
                BasketItems = await _db.Basket
                .Where(b => b.EmailAddress == emailAdress)
                .Select(b => new BasketProductViewModel
                {
                    BasketId = b.BasketId,
                    Quantity = b.Quantity,
                    ProductName = b.Product.Name,
                    Description = b.Product.Description
                })
                .ToListAsync();


            }
        
        }
        
        public async Task<IActionResult> OnPostCheckoutAsync()
        {
            string emailAdress = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(emailAdress))
            {
                return RedirectToPage("/Login");
            }

            var basketItems = await _db.Basket.Where(b => b.EmailAddress == emailAdress).ToListAsync();
            if (!basketItems.Any())
            {
                return Page(); // No items in the basket
            }

            // Create a new order
            var order = new OrderModel { EmailAddress = emailAdress };
            _db.Orders.Add(order);
            await _db.SaveChangesAsync(); // Save to get OrderId

            // Add items to OrderItems
            foreach (var item in basketItems)
            {
                var orderItem = new OrderItem
                {
                    OrderId = order.OrderId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                };
                _db.OrderItems.Add(orderItem);

                // Remove the item from the basket
                _db.Basket.Remove(item);
            }

            await _db.SaveChangesAsync(); // Final save to update database

            return RedirectToPage("/OrderConfirmation", new { orderId = order.OrderId });

        }
        
    }
}
