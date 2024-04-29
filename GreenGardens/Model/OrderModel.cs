using System.ComponentModel.DataAnnotations;

namespace GreenGardens.Model
{
    public class OrderModel
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        public string CustomerId { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        public virtual List<OrderItem> Items { get; set; } = new List<OrderItem>();

        public decimal Total { get; set; }  

       
    }
}
