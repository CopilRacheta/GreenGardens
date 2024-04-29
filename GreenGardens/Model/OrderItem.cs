using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenGardens.Model
{
    public class OrderItem
    {

        [Key]
        public int OrderItemId {  get; set; }

        [Required]
        public int OrderId {  get; set; }

        [ForeignKey("OrderId")]
        public OrderModel Order { get; set; }

        [Required]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public ProductModel Product { get; set; }

        public int Quantity {  get; set; }
    }
}
