using System.ComponentModel.DataAnnotations;
namespace GreenGardens.Model
{
    public class OrderModel
    {
        [Key]
        public int Id { get; set; }

        public int CustomerId { get; set; }
        public  CustomersModel Customers { get; set; }

        public int ProductId { get; set; }

        public ProductModel Product { get; set; }
    }
}
