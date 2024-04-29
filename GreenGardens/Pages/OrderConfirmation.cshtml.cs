using GreenGardens.Data;
using GreenGardens.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenGardens.Pages
{
    public class OrderConfirmationModel : PageModel
    {
        private readonly AppDbContext _context;

        public int OrderId {  get; set; }
        public OrderModel Order { get; set; }

        public List<OrderItem> OrderItems { get; set; }

        public OrderConfirmationModel(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync (int orderId)
        {
            OrderId = orderId;
            Order = await _context.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (Order == null)
            {
                return NotFound();
            }

            OrderItems = Order.Items.ToList();
            return Page();
        }
        
    }
}
