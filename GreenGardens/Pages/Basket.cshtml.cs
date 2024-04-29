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
    public class BasketModel : PageModel
    {
        private readonly AppDbContext _db;

        public BasketModel(AppDbContext db)
        {
            _db = db;
        }

        public List<BasketProductViewModel> BasketItem { get; set; }
        public string CustomerId { get; internal set; }
        public int ProductId { get; internal set; }

        public class BasketProductViewModel
        {
            public int BasketId { get; set; }
            public int Quantity { get; set; }

            public string ProductName { get; set; }

            public string Description { get; set; }

        }

        public async Task OnGetAsync()
        {
            string customerId = HttpContext.Session.GetString("CustomerId");

            if(!string.IsNullOrEmpty(customerId) )
            {
                BasketItem = await _db.Baskets
                .Where(b => b.CustomerId.ToString() == customerId)
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
            string customerId = HttpContext.Session.GetString("CustomerId");
            if (string.IsNullOrEmpty(customerId))
            {
                return RedirectToPage("/Login");
            }

            var basketItems = await _db.Baskets.Where(b => b.CustomerId.ToString() == customerId).ToListAsync();
            if (!basketItems.Any())
            {
                return Page(); // No items in the basket
            }

            // Create a new order
            var order = new OrderModel { CustomerId = customerId };
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
                _db.Baskets.Remove(item);
            }

            await _db.SaveChangesAsync(); // Final save to update database

            return RedirectToPage("/OrderConfirmation", new { orderId = order.OrderId });
        }

    }
}
